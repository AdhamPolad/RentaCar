using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCar.Buisness.Dtos.Engin;

namespace TestRentaCar.Buisness.Validations.Engin
{
    public class PostEnginDtoValidator : AbstractValidator<PostEnginDto>
    {
        public PostEnginDtoValidator()
        {
            RuleFor(x => x.EnginType)
                .NotEmpty().WithMessage("Mühərrik növü boş ola bilməz.");

        }
    }
}
