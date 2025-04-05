using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Certifications.Commands;

namespace CvAnalysisSystem.Application.CQRS.Certifications.Validators;

public class DeleteCertificationValidator : AbstractValidator<DeleteCertificationCommand>
{
    public DeleteCertificationValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}
