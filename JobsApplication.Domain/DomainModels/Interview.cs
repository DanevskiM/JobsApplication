﻿namespace JobsApplication.Domain.DomainModels
{
    public class Interview : BaseEntity
    {
        public DateOnly InterviewDate { get; set; }
        public string InterviewType { get; set; }
        public string Notes { get; set; }
        public JobPosition? JobPosition { get; set; }
        public Guid JobPositionId { get; set; }
    }
}
