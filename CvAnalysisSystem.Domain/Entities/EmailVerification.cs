using CvAnalysisSystem.Domain.BaseEntities;

namespace CvAnalysisSystem.Domain.Entities
{
    public class EmailVerification : BaseEntity
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
