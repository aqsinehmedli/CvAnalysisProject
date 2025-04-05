using CvAnalysisSystem.Application.CQRS.Education.Commands;
using FluentValidation;

namespace CvAnalysisSystem.Application.CQRS.Education.Validators;

public class CreateEducationValidator : AbstractValidator<CreateEducationCommand>
{
    public CreateEducationValidator()
    {
        RuleFor(x => x.CvModelId)
            .GreaterThan(0).WithMessage("CV reference (CvModelId) is required.");

        RuleFor(x => x.School)
            .NotEmpty().WithMessage("School name is required.")
            .MaximumLength(255).WithMessage("School name must not exceed 255 characters.");

        RuleFor(x => x.Degree)
            .NotEmpty().WithMessage("Degree field is required.")
            .MaximumLength(255).WithMessage("Degree must not exceed 255 characters.");

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
