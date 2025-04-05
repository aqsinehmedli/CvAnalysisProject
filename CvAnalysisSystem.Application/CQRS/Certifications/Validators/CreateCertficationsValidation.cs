using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Certifications.Commands;
using CvAnalysisSystem.Application.CQRS.Certifications.Handlers.Commands;

namespace CvAnalysisSystem.Application.CQRS.Certifications.Validators;

public class CreateCertificationValidator : AbstractValidator<CreateCertificationCommand>
{
    public CreateCertificationValidator()
    {
        RuleFor(x => x.CvModelId)
            .GreaterThan(0).WithMessage("CvModelId must be greater than zero.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        RuleFor(x => x.Organization)
            .NotEmpty().WithMessage("Organization is required.")
            .MaximumLength(255).WithMessage("Organization cannot exceed 255 characters.");

        RuleFor(x => x.IssueDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Issue date cannot be in the future.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.");

        RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("CreateDate is required.");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted must be specified.");
    }
}
