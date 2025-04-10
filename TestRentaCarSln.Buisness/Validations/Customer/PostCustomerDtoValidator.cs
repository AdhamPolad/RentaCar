using FluentValidation;
using TestRentaCar.Buisness.Dtos.Customer;

namespace TestRentaCar.Buisness.Validations.Customer
{
    public class PostCustomerDtoValidator : AbstractValidator<PostCustomerDto>
    {
        public PostCustomerDtoValidator()
        {
            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Ünvan boş ola bilməz.");

            RuleFor(x => x.DriverLisenceNumber)
                .NotEmpty().WithMessage("Sürücülük vəsiqəsi nömrəsi boş ola bilməz.");
        }
    }
}
