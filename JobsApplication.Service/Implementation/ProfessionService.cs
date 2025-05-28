using JobsApplication.Domain.DomainModels;
using JobsApplication.Repository.Interface;
using JobsApplication.Service.Interface;

namespace JobsApplication.Service.Implementation 
{
    public class ProfessionService : IProfessionService
    {
        private readonly IRepository<Profession> _professionRepository;
        private readonly IRepository<CandidateInProfession> _candidateInProfessionRepository;
        private readonly IApplicationService _applicationService;

        public ProfessionService(IRepository<Profession> professionRepository, IRepository<CandidateInProfession> candidateInProfessionRepository, IApplicationService applicationService)
        {
            _professionRepository = professionRepository;
            _candidateInProfessionRepository = candidateInProfessionRepository;
            _applicationService = applicationService;
        }

        public Profession Create(string userId)
        {
            // TODO: Implement method
            // Hint: Look at auditory exercises: OrderProducts method in ShoppingCartService

            // Get all Applications by current user
            // Create new Profession and insert in database
            // Create new CandidatesInProfession using Candidates from the Applications and insert in database
            // Delete all Applications
            // Return Profession

            throw new NotImplementedException();
        }

        // Bonus task
        public Profession GetDetailsById(Guid id)
        {
            // TODO: Implement method
            throw new NotImplementedException();
        }
    }
}
