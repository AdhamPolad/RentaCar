using FluentValidation;
using TestRentaCar.Buisness.Dtos.CarDetails;
using TestRentaCar.Buisness.Validations.Engin;

namespace TestRentaCar.Buisness.Validations.CarDetail
{
    public class PostCarDetailValidator : AbstractValidator<PostCarDetail>
    {
        public PostCarDetailValidator()
        {
            RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0).WithMessage("Yürüş 0-dan kiçik ola bilməz.");

            RuleFor(x => x.DoorsCount)
                .InclusiveBetween(2, 6).WithMessage("Qapı sayı 2 ilə 6 arasında olmalıdır.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Rəng boş ola bilməz.")
                .MaximumLength(50).WithMessage("Rəng maksimum 50 simvol ola bilər.");

            RuleFor(x => x.PostEnginDto)
                .NotNull().WithMessage("Mühərrik detalları boş ola bilməz.")
                .SetValidator(new PostEnginDtoValidator());

            RuleFor(x => x.Transmision)
            .IsInEnum().WithMessage("Keçərli bir transmissiya tipi seçilməlidir.");
        }
    }
}
