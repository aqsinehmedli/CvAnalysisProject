using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Certifications.Handlers.Commands
{
    public class UpdateCertificationCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Organization { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
    }
}
