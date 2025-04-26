namespace CvAnalysisSystem.Domain.Entities;

public class Cv
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public string PdfFilePath { get; set; }
    public string Education { get; set; }
    public string WorkExperience { get; set; }
    public string Skills { get; set; }
    public string Languages { get; set; }
    public string Certifications { get; set; }
    public string Status { get; set; }
    public DateTime UploadDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string AiAnalysis { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedDate { get; set; }
}