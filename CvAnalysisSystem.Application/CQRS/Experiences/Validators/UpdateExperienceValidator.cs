using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Experiences.Commands;

namespace CvAnalysisSystem.Application.CQRS.Experiences.Validators;

public class UpdateExperienceValidator : AbstractValidator<UpdateExperienceCommand>
{
    public UpdateExperienceValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Experience ID is required.");

        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Company name cannot be empty.")
            .MaximumLength(255).WithMessage("Company name must not exceed 255 characters.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(255).WithMessage("Position name must not exceed 255 characters.");

        RuleFor(x => x.StartYear)
            .InclusiveBetween(1900, 2100).WithMessage("Start year must be between 1900 and 2100.");

        RuleFor(x => x.EndYear)
            .InclusiveBetween(1900, 2100).WithMessage("End year must be between 1900 and 2100.")
            .GreaterThanOrEqualTo(x => x.StartYear).WithMessage("End year cannot be earlier than start year.");

        RuleFor(x => x.UpdatedBy)
            .NotEmpty().WithMessage("UpdatedBy is required.");

        RuleFor(x => x.UpdatedDate)
            .NotEmpty().WithMessage("Updated date is required.");
    }
}
