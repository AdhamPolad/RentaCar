using FluentValidation;
using TestRentaCar.Buisness.Dtos.User;
using TestRentaCar.Buisness.Validations.Customer;

namespace TestRentaCar.Buisness.Validations.User
{
    public class CreateCustomerUserDtoValidator : AbstractValidator<CreateCustomerUserDto>
    {
        public CreateCustomerUserDtoValidator()
        {
            RuleFor(x => x.User)
            .NotNull().WithMessage("İstifadəçi məlumatları boş ola bilməz.")
            .SetValidator(new CreateUserDtoValidator()); // CreateUserDto üçün validasiya tətbiq et

            RuleFor(x => x.Customer)
                .NotNull().WithMessage("Müştəri məlumatları boş ola bilməz.")
                .SetValidator(new PostCustomerDtoValidator()); // PostCustomerDto üçün validasiya tətbiq et
        }

    }
}
