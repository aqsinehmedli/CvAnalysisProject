using CvAnalysisSystem.Domain.BaseEntities;
using CvAnalysisSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CvAnalysisSystem.Domain.Entities;

public class CvModel:BaseEntity
{
    public int UserId {  get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string LinkedInUrl { get; set; }
    public string GitHubUrl { get; set; }
    public TemplateType TemplateType { get; set; } // seçilmiş şablonun adı

    // Navigation properties (relation)
    public List<Education> Educations { get; set; }
    public List<Experience> Experiences { get; set; }
    public List<Skill> Skills { get; set; }
    public List<Certifications> Certifications { get; set; }
    public List<Language> Languages { get; set; }
    // Əgər login sistemi varsa:
    //[ForeignKey("UserId")]
    //public User User { get; set; }
}
