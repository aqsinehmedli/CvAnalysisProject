using CvAnalysisSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CvAnalysisSystem.Domain.Entities;

public class Language
{
    public int Id { get; set; }
    public int CvModelId { get; set; }
    public string LanguageName { get; set; }
    public FluencyLevels FluencyLevel { get; set; }
    [ForeignKey("CvModelId")]
    public CvModel CvModel { get; set; }
}