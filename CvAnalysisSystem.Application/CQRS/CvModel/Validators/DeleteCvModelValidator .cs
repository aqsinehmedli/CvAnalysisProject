using FluentValidation;
using CvAnalysisSystem.Application.CQRS.CvModels.Commands;

namespace CvAnalysisSystem.Application.CQRS.CvModels.Validators;

public class DeleteCvModelValidator : AbstractValidator<DeleteCvModelCommand>
{
    public DeleteCvModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("A valid CV ID must be provided for deletion.");
    }
}
