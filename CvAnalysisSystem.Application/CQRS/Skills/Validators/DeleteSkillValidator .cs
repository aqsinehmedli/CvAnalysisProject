using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Skills.Commands;

namespace CvAnalysisSystem.Application.CQRS.Skills.Validators;

public class DeleteSkillValidator : AbstractValidator<DeleteSkillCommand>
{
    public DeleteSkillValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Skill ID is required for deletion.");
    }
}
