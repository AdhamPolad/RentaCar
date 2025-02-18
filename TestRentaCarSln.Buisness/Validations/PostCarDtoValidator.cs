using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Car;

namespace TestRentaCarSln.Buisness.Validations
{
    public class PostCarDtoValidator : AbstractValidator<PostCarDto>
    {
        public PostCarDtoValidator()
        {
            RuleFor(x => x.Brand)
                .NotNull().WithMessage("Brand məlumatı boş ola bilməz.")
                .SetValidator(new PostBrandDtoValidator());

            RuleFor(x => x.PricePerDay)
                .GreaterThan(0).WithMessage("PricePerDay 0-dan böyük olmalıdır.");

            RuleFor(x => x.IsAviable)
                .NotNull().WithMessage("IsAviable dəyəri boş ola bilməz.");
        }

    }
}
