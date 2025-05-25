using CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;
using FluentValidation;

namespace CvAnalysisSystem.Application.CQRS.Users.Validators
{
    public class UpdateRequestValidator : AbstractValidator<Update.Command>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("İstifadəçi ID-si düzgün deyil");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz")
                .MaximumLength(50).WithMessage("Ad 50 simvoldan çox ola bilməz");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad boş ola bilməz")
                .MaximumLength(50).WithMessage("Soyad 50 simvoldan çox ola bilməz");

            RuleFor(x => x.FatherName)
                .NotEmpty().WithMessage("Ata adı boş ola bilməz")
                .MaximumLength(50).WithMessage("Ata adı 50 simvoldan çox ola bilməz");

            RuleFor(x => x.MobilePhone)
                .NotEmpty().WithMessage("Mobil nömrə boş ola bilməz")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Mobil nömrə formatı düzgün deyil");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email formatı düzgün deyil");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Ünvan boş ola bilməz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvol olmalıdır");

            RuleFor(x => x.Gender)
                .Must(g => g == null || (g >= 0 && g <= 1))
                .WithMessage("Cinsiyyət dəyəri düzgün deyil");


            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("Doğum tarixi mütləqdir")
                .LessThan(DateTime.Now).WithMessage("Doğum tarixi gələcəkdə ola bilməz");
        }
    }
}
