using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Brand;

namespace TestRentaCarSln.Buisness.Validations
{
    public class PostBrandDtoValidator : AbstractValidator<PostBrandDto>
    {
        public PostBrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .MinimumLength(2).WithMessage("Ad ən azı 2 simvoldan ibarət olmalıdır.")
                .MaximumLength(50).WithMessage("Ad maksimum 50 simvol ola bilər.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model boş ola bilməz.")
                .MinimumLength(2).WithMessage("Model ən azı 2 simvoldan ibarət olmalıdır.")
                .MaximumLength(50).WithMessage("Model maksimum 50 simvol ola bilər.");

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1900).WithMessage("İl 1900-dən kiçik ola bilməz.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage($"İl {DateTime.Now.Year}-dən böyük ola bilməz.");
        }
    }
}
