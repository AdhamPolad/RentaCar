using FluentValidation;
using TestRentaCar.Buisness.Dtos.Discount;

namespace TestRentaCar.Buisness.Validations.Discount
{
    public class UpdateDiscountDtoValidator : AbstractValidator<UpdateDiscountDto>
    {
        public UpdateDiscountDtoValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id 0-dan böyük olmalıdır.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThan(0).WithMessage("Endirim məbləği 0-dan böyük olmalıdır.");

            RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlama tarixi boş ola bilməz.")
            .LessThan(x => x.EndDate).WithMessage("Başlama tarixi bitmə tarixindən kiçik olmalıdır.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Bitmə tarixi boş ola bilməz.")
                .GreaterThan(x => x.StartDate).WithMessage("Bitmə tarixi başlama tarixindən böyük olmalıdır.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive dəyəri göstərilməlidir.");
        }
    }

}

