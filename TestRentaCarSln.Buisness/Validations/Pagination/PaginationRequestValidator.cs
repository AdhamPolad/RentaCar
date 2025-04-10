using FluentValidation;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Buisness.Validations.Pagination
{
    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            // PageNumber minimum 1 olmalıdır
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than or equal to 1.");

            // PageSize minimum 1 olmalıdır və maksimum 100 olmalıdır
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize must be greater than or equal to 1.");
        }
    }
}
