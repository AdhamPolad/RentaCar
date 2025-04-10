using FluentValidation;
using TestRentaCar.Buisness.Dtos.Insurance;

namespace TestRentaCar.Buisness.Validations.Insurance
{
    public class PostInsuranceDtoValidator : AbstractValidator<PostInsuranceDto>
    {
        public PostInsuranceDtoValidator()
        {
            RuleFor(x => x.Price)
             .GreaterThan(0).WithMessage("Sığorta qiyməti 0-dan böyük olmalıdır.");

            RuleFor(x => x.CarInsuranceType)
            .IsInEnum().WithMessage("Düzgün bir CarInsuranceType dəyəri seçin.");
        }
    }
}
