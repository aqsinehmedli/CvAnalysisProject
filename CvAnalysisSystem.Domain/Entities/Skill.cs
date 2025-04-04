using CvAnalysisSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CvAnalysisSystem.Domain.Entities;


public class Skill
{
    public int Id { get; set; }
    public int CvModelId { get; set; }
    public string SkillName { get; set; }
    public ProfiencyLevel ProficiencyLevel { get; set; }
    [ForeignKey("CvModelId")]
    public CvModel CvModel { get; set; }
}
