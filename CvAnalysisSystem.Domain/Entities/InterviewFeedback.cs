using CvAnalysisSystem.Domain.Enums;

namespace CvAnalysisSystem.Domain.Entities;
public class Interview_feedback
{
    public int Id { get; set; }
    public int InterviewId { get; set; }
    public int EmployerId { get; set; }
    public string FeedbackText { get; set; }
    public Raitings Rating { get; set; }
    public DateTime FeedBackDate { get; set; }
}
