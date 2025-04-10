using FluentValidation;
using TestRentaCar.Buisness.Dtos.Model;
using TestRentaCar.Buisness.Validations.Brand;

namespace TestRentaCar.Buisness.Validations.Model
{
    class PostModelDtoValidator : AbstractValidator<PostModelDto>
    {
        public PostModelDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Model adı boş ola bilməz.")
            .MaximumLength(100).WithMessage("Model adı maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year).WithMessage($"İl 1900 ilə {DateTime.UtcNow.Year} arasında olmalıdır.");

        }

    }
}
