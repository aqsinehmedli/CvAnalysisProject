using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Cv.DTOs
{
    public record CreateDtoCv
    {
        public string PdfFilePath { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; }
        public string Skills { get; set; }
        public string Languages { get; set; }
        public string Certifications { get; set; }
        public string Status { get; set; }
    }
}
