using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Experiences.Commands;

namespace CvAnalysisSystem.Application.CQRS.Experiences.Validators;

public class CreateExperienceValidator : AbstractValidator<CreateExperienceCommand>
{
    public CreateExperienceValidator()
    {
        RuleFor(x => x.CvModelId)
            .GreaterThan(0).WithMessage("CV reference (CvModelId) is required.");

        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(255).WithMessage("Company name must not exceed 255 characters.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(255).WithMessage("Position name must not exceed 255 characters.");

        RuleFor(x => x.StartYear)
            .InclusiveBetween(1900, 2100).WithMessage("Start year must be between 1900 and 2100.");

        RuleFor(x => x.EndYear)
            .InclusiveBetween(1900, 2100).WithMessage("End year must be between 1900 and 2100.")
            .GreaterThanOrEqualTo(x => x.StartYear).WithMessage("End year cannot be earlier than start year.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.");

        RuleFor(x => x.CreatedDate)
            .NotEmpty().WithMessage("Created date is required.");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted field must be specified.");
    }
}
