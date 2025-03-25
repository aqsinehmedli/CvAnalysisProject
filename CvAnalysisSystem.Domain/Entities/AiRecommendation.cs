namespace CvAnalysisSystem.Domain.Entities;

public class Ai_recommendation
{
    public int Id { get; set; }
    public int CvId { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public DateTime CreatedDate { get; set; }

}