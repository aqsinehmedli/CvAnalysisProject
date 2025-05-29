//using CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CvAnalysisSystem.Application.CQRS.Users.Validators
//{
//    public class RefreshTokenRequestValidator : AbstractValidator<RefreshToken.RefreshTokenReuqest>
//    {
//        public RefreshTokenRequestValidator()
//        {
//            RuleFor(x => x.Token)
//                .NotEmpty().WithMessage("Refresh token boş ola bilməz")
//                .MinimumLength(10).WithMessage("Refresh tokenin uzunluğu ən azı 10 simvol olmalıdır");
//        }
//    }
//}
