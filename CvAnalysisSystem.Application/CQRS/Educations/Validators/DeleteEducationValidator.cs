using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Education.Commands;

namespace CvAnalysisSystem.Application.CQRS.Education.Validators;

public class DeleteEducationValidator : AbstractValidator<DeleteEducationCommand>
{
    public DeleteEducationValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Education ID is required for deletion.");
    }
}
