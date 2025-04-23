using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Cv.DTOs
{
    public record CreateCvDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public int TemplateId { get; set; }
        public List<EducationDto> Educations { get; set; }
        public List<ExperienceDto> Experiences { get; set; }
        public List<SkillDto> Skills { get; set; }
        public List<CertificationDto> Certifications { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public string ProfileImageBase64 { get; set; } // Base64 image
        public byte[] Pdf {  get; set; }
    }
}
