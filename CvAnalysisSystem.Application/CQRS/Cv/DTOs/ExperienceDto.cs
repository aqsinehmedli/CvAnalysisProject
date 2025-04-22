namespace CvAnalysisSystem.Application.CQRS.Cv.DTOs;

public record ExperienceDto
{
    public string Company { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
