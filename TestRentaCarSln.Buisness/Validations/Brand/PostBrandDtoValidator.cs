using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Brand;

namespace TestRentaCar.Buisness.Validations.Brand
{
    public class PostBrandDtoValidator : AbstractValidator<PostBrandDto>
    {
        public PostBrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .MinimumLength(2).WithMessage("Ad ən azı 2 simvoldan ibarət olmalıdır.")
                .MaximumLength(50).WithMessage("Ad maksimum 50 simvol ola bilər.");

        }
    }
}
