using FluentValidation;
using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCar.Buisness.Validations.CarDetail;

namespace TestRentaCar.Buisness.Validations.Car
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarDto>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.ModelId)
             .GreaterThan(0).WithMessage("Model ID 0-dan böyük olmalıdır.");

            RuleFor(x => x.PricePerDay)
                .GreaterThan(0).WithMessage("Günlük qiymət 0-dan böyük olmalıdır.");

            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("Nömrə nişanı boş ola bilməz.");

            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("Filial ID-si 0-dan böyük olmalıdır.");

            RuleFor(x => x.InsuranceId)
                .GreaterThan(0).WithMessage("Sığorta ID-si 0-dan böyük olmalıdır.");

            RuleFor(x => x.UpdateCarDetail)
                .NotNull().WithMessage("Avtomobil detalları boş ola bilməz.")
                .SetValidator(new UpdateCarDetailValidator());
        }
    }
}
