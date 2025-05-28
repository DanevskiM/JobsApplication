using JobsApplication.Domain.IdentityModels;

namespace JobsApplication.Domain.DomainModels
{
    public class Profession : BaseEntity
    {
        public string UserId { get; set; }
        public DateTime DateCreation { get; set; }
        public JobsApplicationUser User { get; set; }
        public virtual ICollection<CandidateInProfession> CandidateInProfessions { get; set; }
    }
}
