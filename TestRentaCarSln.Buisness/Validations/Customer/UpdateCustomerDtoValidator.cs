using FluentValidation;
using TestRentaCar.Buisness.Dtos.Customer;

namespace TestRentaCar.Buisness.Validations.Customer
{
    public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerDtoValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id sıfırdan böyük olmalıdır.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad və soyad boş ola bilməz.")
                .MaximumLength(100).WithMessage("Ad və soyad maksimum 100 simvol olmalıdır.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Ünvan boş ola bilməz.")
                .MaximumLength(200).WithMessage("Ünvan maksimum 200 simvol ola bilər.");

            RuleFor(x => x.DriverLisenceNumber)
                .NotEmpty().WithMessage("Sürücülük vəsiqəsinin nömrəsi boş ola bilməz.");
        }

    }
}
