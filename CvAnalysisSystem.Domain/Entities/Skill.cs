using CvAnalysisSystem.Domain.Enums;

namespace CvAnalysisSystem.Domain.Entities;


public class Skill
{
    public int Id { get; set; }
    public int CvId { get; set; }
    public string SkillName { get; set; }
    public ProfiencyLevel ProficiencyLevel { get; set; }
}
