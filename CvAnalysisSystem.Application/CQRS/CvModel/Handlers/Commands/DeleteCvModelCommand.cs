using MediatR;

namespace CvAnalysisSystem.Application.CQRS.CvModels.Commands;

public class DeleteCvModelCommand : IRequest
{
    public int Id { get; set; }
}
