namespace CvAnalysisSystem.Domain.Entities;

public class Session
{
    public int Id { get; set; }
    public int InterviewId { get; set; }
    public string SessionType { get; set; }
    public DateTime SessionDate { get; set; }
    public string SessionLink { get; set; }

}