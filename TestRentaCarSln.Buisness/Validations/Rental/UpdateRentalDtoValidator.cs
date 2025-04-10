using FluentValidation;
using TestRentaCar.Buisness.Dtos.Rental;

namespace TestRentaCar.Buisness.Validations.Rental
{
    public class UpdateRentalDtoValidator : AbstractValidator<UpdateRentalDto>
    {
        public UpdateRentalDtoValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id müsbət bir dəyər olmalıdır.");

            RuleFor(x => x.CarId)
                .GreaterThan(0)
                .WithMessage("CarId müsbət bir dəyər olmalıdır.");

            RuleFor(x => x.RentalDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("RentalDate indiki tarixdən əvvəl ola bilməz.");

            RuleFor(x => x.ReturnDate)
                .GreaterThan(x => x.RentalDate)
                .WithMessage("ReturnDate, RentalDate-dən sonra olmalıdır.");
        }

    }
}
