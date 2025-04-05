using MediatR;

namespace CvAnalysisSystem.Application.CQRS.Certifications.Commands
{
    public class DeleteCertificationCommand : IRequest<bool> 
    {
        public int Id { get; set; }
    }
}
