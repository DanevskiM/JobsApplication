namespace JobsApplication.Domain.DomainModels
{
    public class CandidateInProfession : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public Guid ProfessionId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual Profession Profession { get; set; }
        public int Quantity { get; set; }
    }
}
