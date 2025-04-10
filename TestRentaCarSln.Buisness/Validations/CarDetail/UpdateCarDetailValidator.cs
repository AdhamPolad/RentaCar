using FluentValidation;
using TestRentaCar.Buisness.Dtos.CarDetails;

namespace TestRentaCar.Buisness.Validations.CarDetail
{
    public class UpdateCarDetailValidator : AbstractValidator<UpdateCarDetail>
    {
        public UpdateCarDetailValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id 0-dan böyük olmalıdır.");

            RuleFor(x => x.EngineId)
                .GreaterThan(0).WithMessage("Mühərrik ID-si 0-dan böyük olmalıdır.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0).WithMessage("Yürüş 0-dan kiçik ola bilməz.");

            RuleFor(x => x.DoorsCount)
                .InclusiveBetween(2, 6).WithMessage("Qapı sayı 2 ilə 6 arasında olmalıdır.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Rəng boş ola bilməz.");

            RuleFor(x => x.Transmision)
            .IsInEnum().WithMessage("Keçərli bir transmissiya tipi seçilməlidir.");
        }

    }
}
