using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Experiences.Commands;

namespace CvAnalysisSystem.Application.CQRS.Experiences.Validators;

public class DeleteExperienceValidator : AbstractValidator<DeleteExperienceCommand>
{
    public DeleteExperienceValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Experience ID is required for deletion.");
    }
}
