using MediatR;

namespace CvAnalysisSystem.Application.CQRS.Education.Commands;

public class DeleteEducationCommand : IRequest
{
    public int Id { get; set; }
}
