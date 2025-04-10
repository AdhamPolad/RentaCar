using FluentValidation;
using TestRentaCar.Buisness.Dtos.Review;

namespace TestRentaCar.Buisness.Validations.Review
{
    public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id sıfırdan böyük olmalıdır.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Şərh boş ola bilməz.");
        }

    }
}
