using FluentValidation;
using TestRentaCar.Buisness.Dtos.Maintenance;

namespace TestRentaCar.Buisness.Validations.Maintenance
{
    public class PostMaintenanceDtoValidator : AbstractValidator<PostMaintenanceDto>
    {
        public PostMaintenanceDtoValidator()
        {
            RuleFor(x => x.RentalId)
            .GreaterThan(0).WithMessage("RentalId sıfırdan böyük olmalıdır.");

            RuleFor(x => x.MaintenanceDate)
                .NotEmpty().WithMessage("Bakım tarixi boş ola bilməz.");

            RuleFor(x => x.TotalCost)
                .GreaterThan(0).WithMessage("Ümumi xərc müsbət olmalıdır.");

            RuleFor(x => x.InsuranceCoverage)
                .LessThanOrEqualTo(x => x.TotalCost).WithMessage("Sığorta məbləği ümumi xərci keçə bilməz.");
        }

    }
}
