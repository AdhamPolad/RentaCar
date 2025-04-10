using FluentValidation;
using TestRentaCar.Buisness.Dtos.Brand;

namespace TestRentaCar.Buisness.Validations.Brand
{
    public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .MinimumLength(2).WithMessage("Ad ən azı 2 simvoldan ibarət olmalıdır.")
                .MaximumLength(50).WithMessage("Ad maksimum 50 simvol ola bilər.");
        }
    }
}
