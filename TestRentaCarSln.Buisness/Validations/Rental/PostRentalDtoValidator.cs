using FluentValidation;
using TestRentaCar.Buisness.Dtos.Rental;

namespace TestRentaCar.Buisness.Validations.Rental
{
    public class PostRentalDtoValidator : AbstractValidator<PostRentalDto>
    {
        public PostRentalDtoValidator()
        {
            RuleFor(x => x.CarId)
           .GreaterThan(0)
           .WithMessage("CarId müsbət bir dəyər olmalıdır.");

            RuleFor(x => x.RentalDate)
                .NotEmpty().WithMessage("RentalDate boş ola bilməz");

            RuleFor(x => x.ReturnDate)
                .GreaterThan(x => x.RentalDate)
                .WithMessage("ReturnDate RentalDate-dən sonra olmalıdır.");
        }

    }
}
