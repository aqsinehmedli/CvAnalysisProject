namespace CvAnalysisSystem.Domain.Entities
{
     public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
