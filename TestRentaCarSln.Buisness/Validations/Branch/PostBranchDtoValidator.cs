using FluentValidation;
using TestRentaCar.Buisness.Dtos.Branch;

namespace TestRentaCar.Buisness.Validations.Branch
{
    public class PostBranchDtoValidator : AbstractValidator<PostBranchDto>
    {
        public PostBranchDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
        }

    }
}
