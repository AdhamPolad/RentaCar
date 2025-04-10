using FluentValidation;
using TestRentaCar.Buisness.Dtos.Payment;

namespace TestRentaCar.Buisness.Validations.Payment
{
    public class UpdatePaymentDtoValidator : AbstractValidator<UpdatePaymentDto>
    {
        public UpdatePaymentDtoValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id müsbət olmalıdır.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount 0-dan böyük olmalıdır.");

            RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage("PaymentDate bos ola bilməz.");

            RuleFor(x => x.PaymentStatus)
                .IsInEnum().WithMessage("Yanlış ödəniş statusu seçildi.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum().WithMessage("Yanlış ödəniş metodu seçildi.");

        }
    }
}
