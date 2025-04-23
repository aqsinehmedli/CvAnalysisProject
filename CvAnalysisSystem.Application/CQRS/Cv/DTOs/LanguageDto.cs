using CvAnalysisSystem.Domain.Enums;

namespace CvAnalysisSystem.Application.CQRS.Cv.DTOs;

public class LanguageDto
{
    public string LanguageName { get; set; }
    public FluencyLevels FluencyLevel { get; set; }
}
