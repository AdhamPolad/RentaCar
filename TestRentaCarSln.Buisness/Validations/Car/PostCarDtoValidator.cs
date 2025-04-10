using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCar.Buisness.Validations;
using TestRentaCar.Buisness.Validations.CarDetail;
using TestRentaCarSln.Buisness.Dtos.Car;

namespace TestRentaCar.Buisness.Validations.Car
{
    public class PostCarDtoValidator : AbstractValidator<PostCarDto>
    {
        public PostCarDtoValidator()
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

            RuleFor(x => x.CarDetails)
                .NotNull().WithMessage("Avtomobil detalları boş ola bilməz.")
                .SetValidator(new PostCarDetailValidator());

            RuleFor(x => x.CarCatagory)
            .IsInEnum().WithMessage("Keçərli bir avtomobil kateqoriyası seçilməlidir.");
        }

    }
}
