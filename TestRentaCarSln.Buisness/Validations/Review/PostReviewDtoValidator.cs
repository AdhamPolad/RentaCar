using FluentValidation;
using TestRentaCar.Buisness.Dtos.Review;

namespace TestRentaCar.Buisness.Validations.Review
{
    public class PostReviewDtoValidator : AbstractValidator<PostReviewDto>
    {
        public PostReviewDtoValidator()
        {
            RuleFor(x => x.CarId)
            .GreaterThan(0).WithMessage("CarId sıfırdan böyük olmalıdır.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Şərh boş ola bilməz.");
        }

    }
}
