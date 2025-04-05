using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Certifications.Handlers.Commands;

namespace CvAnalysisSystem.Application.CQRS.Certifications.Validators;

public class UpdateCertificationValidator : AbstractValidator<UpdateCertificationCommand>
{
    public UpdateCertificationValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        RuleFor(x => x.Organization)
            .NotEmpty().WithMessage("Organization is required.")
            .MaximumLength(255).WithMessage("Organization cannot exceed 255 characters.");

        RuleFor(x => x.IssueDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Issue date cannot be in the future.");

        RuleFor(x => x.UpdatedBy)
            .NotEmpty().WithMessage("UpdatedBy is required.");

        RuleFor(x => x.UpdateDate)
            .NotEmpty().WithMessage("UpdateDate is required.");
    }
}
