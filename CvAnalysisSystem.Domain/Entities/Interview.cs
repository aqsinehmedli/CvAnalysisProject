namespace CvAnalysisSystem.Domain.Entities;
public class Interview
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime InterviewDate { get; set; }
    public string InterviewType { get; set; }
    public string Status { get; set; }
    public string Feedback { get; set; }
}
