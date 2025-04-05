using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Certifications.Handlers.Commands
{
    public class CreateCertificationCommand : IRequest<int>
    {
        public int CvModelId { get; set; }
        public string Title { get; set; } = null!;
        public string Organization { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
