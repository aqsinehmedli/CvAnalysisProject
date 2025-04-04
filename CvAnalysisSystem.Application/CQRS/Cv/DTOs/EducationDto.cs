namespace CvAnalysisSystem.Application.CQRS.Cv.DTOs;

public record EducationDto
{
    public string School { get; set; }
    public string Degree { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime EndYear { get; set; }
}
