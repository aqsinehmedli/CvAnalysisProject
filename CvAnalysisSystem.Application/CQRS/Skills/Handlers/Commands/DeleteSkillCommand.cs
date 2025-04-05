using MediatR;

namespace CvAnalysisSystem.Application.CQRS.Skills.Commands;

public class DeleteSkillCommand : IRequest
{
    public int Id { get; set; }
}
