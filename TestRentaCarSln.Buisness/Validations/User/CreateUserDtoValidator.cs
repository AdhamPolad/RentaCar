using FluentValidation;
using TestRentaCar.Buisness.Dtos.User;

namespace TestRentaCar.Buisness.Validations.User
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz.")
            .MinimumLength(4).WithMessage("İstifadəçi adı minimum 4 simvol olmalıdır.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .Matches(@"^[a-zA-ZüÜöÖğĞıİçÇşŞ]+$").WithMessage("Ad yalnız hərflərdən ibarət olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş ola bilməz.")
                .Matches(@"^[a-zA-ZüÜöÖğĞıİçÇşŞ]+$").WithMessage("Soyad yalnız hərflərdən ibarət olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz.");

            RuleFor(x => x.Passwords)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarixi boş ola bilməz.");

        }

    }
}
