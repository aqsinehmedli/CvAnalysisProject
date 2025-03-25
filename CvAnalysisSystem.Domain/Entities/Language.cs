using CvAnalysisSystem.Domain.Enums;

namespace CvAnalysisSystem.Domain.Entities;

public class Language
{
    public int Id { get; set; }
    public int CvId { get; set; }
    public string LanguageName { get; set; }
    public FluencyLevels FluencyLevel { get; set; }
}