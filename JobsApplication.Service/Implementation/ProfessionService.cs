using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace JobsApplication.Service.Implementation 
{
    public class ProfessionService : IProfessionService
    {
        private readonly IRepository<Profession> _professionRepository;
        private readonly IRepository<Application> _applicationRepository;
        private readonly IRepository<CandidateInProfession> _candidateInProfessionRepository;
        private readonly IApplicationService _applicationService;

        public ProfessionService(IRepository<Application> applicationRepository, IRepository<Profession> professionRepository, IRepository<CandidateInProfession> candidateInProfessionRepository, IApplicationService applicationService)
        {
            _professionRepository = professionRepository;
            _candidateInProfessionRepository = candidateInProfessionRepository;
            _applicationService = applicationService;
            _applicationRepository = applicationRepository;
        }

        public Profession Create(string userId)
        {
            var applications = _applicationRepository.GetAll(
                    selector: x => x,
                    predicate: x => x.OwnerId == userId,
                    include: x => x.Include(p => p.Candidate).Include(applications => applications.Owner));
            var newProfession = new Profession
            {
                UserId = userId,
                DateCreation = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                User = applications.First().Owner
            };
            _professionRepository.Insert(newProfession);

            var candidateInProfession = new List<CandidateInProfession>();
            foreach(var application in applications)
            {
                var cip = new CandidateInProfession
                {
                    Id = Guid.NewGuid(),
                    ProfessionId = newProfession.Id,
                    CandidateId = application.CandidateId,
                    Quantity = 1
                };
                _candidateInProfessionRepository.Insert(cip);
                candidateInProfession.Add(cip);
            }
            foreach(var application in applications)
            {
                _applicationRepository.Delete(application);
            }
            return newProfession;
        }
        public Profession GetDetailsById(Guid id)
        {
            var profession = _professionRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id,
                include: x => x
                .Include(p => p.CandidateInProfessions)
                .ThenInclude(cip => cip.Candidate));
            if (profession == null)
            {
                throw new Exception("Profession not found");
            }
            return profession;
        }
    }
}
