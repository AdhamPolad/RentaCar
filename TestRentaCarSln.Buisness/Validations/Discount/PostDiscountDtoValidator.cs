using FluentValidation;
using TestRentaCar.Buisness.Dtos.Discount;

namespace TestRentaCar.Buisness.Validations.Discount
{
    public class PostDiscountDtoValidator : AbstractValidator<PostDiscountDto>
    {
        public PostDiscountDtoValidator()
        {
            RuleFor(x => x.DiscountAmount)
            .GreaterThan(0).WithMessage("Endirim məbləği 0-dan böyük olmalıdır.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Başlama tarixi boş ola bilməz");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("Bitmə tarixi başlama tarixindən böyük olmalıdır.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customerİd boş ola bilməz");
        }

    }
}
