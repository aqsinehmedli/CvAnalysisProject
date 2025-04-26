namespace CvAnalysisSystem.Domain.Entities;

public class Certifications
{
    public int Id { get; set; }
    public int CvModelId { get; set; }
    public string Organization { get; set; }
    //public string IssuedBy { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpiredDate { get; set; }

}
