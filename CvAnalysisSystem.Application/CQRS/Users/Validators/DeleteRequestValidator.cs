using CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Users.Validators;

public class DeleteRequestValidator : AbstractValidator<DeleteCv.CvCommand>
{
    public DeleteRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Silinəcək istifadəçi ID-si düzgün deyil");
    }
}