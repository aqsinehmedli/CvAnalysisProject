using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Education.Commands;

namespace CvAnalysisSystem.Application.CQRS.Education.Validators;

public class UpdateEducationValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Education ID is required.");

        RuleFor(x => x.School)
            .NotEmpty().WithMessage("School name cannot be empty.")
            .MaximumLength(255).WithMessage("School name must not exceed 255 characters.");

        RuleFor(x => x.Degree)
            .NotEmpty().WithMessage("Degree is required.")
            .MaximumLength(255).WithMessage("Degree must not exceed 255 characters.");

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
