using MediatR;

namespace CvAnalysisSystem.Application.CQRS.Experiences.Commands;

public class DeleteExperienceCommand : IRequest
{
    public int Id { get; set; }
}
