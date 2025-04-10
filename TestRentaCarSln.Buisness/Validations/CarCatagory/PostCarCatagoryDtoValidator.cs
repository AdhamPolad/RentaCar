using FluentValidation;
using TestRentaCar.Buisness.Dtos.CarCatagory;

namespace TestRentaCar.Buisness.Validations.CarCatagory
{
    public class PostCarCatagoryDtoValidator : AbstractValidator<PostCarCatagory>
    {
        public PostCarCatagoryDtoValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.");
        }
    }
}
