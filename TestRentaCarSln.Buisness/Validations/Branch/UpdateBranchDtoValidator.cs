using FluentValidation;
using TestRentaCar.Buisness.Dtos.Branch;

namespace TestRentaCar.Buisness.Validations.Branch
{
    class UpdateBranchDtoValidator : AbstractValidator<UpdateBranchDto>
    {
        public UpdateBranchDtoValidator()
        {
            RuleFor(x => x.Id)
           .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
        }

    }
}
