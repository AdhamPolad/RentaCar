using FluentValidation;
using TestRentaCar.Buisness.Dtos.Maintenance;

namespace TestRentaCar.Buisness.Validations.Maintenance
{
    public class UpdateMaintenanceValidator : AbstractValidator<UpdateMaintenanceDto>
    {
        public UpdateMaintenanceValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id sıfırdan böyük olmalıdır.");

            RuleFor(x => x.MaintenanceDate)
                .NotEmpty().WithMessage("Bakım tarixi boş ola bilməz.");

            RuleFor(x => x.TotalCost)
                .GreaterThan(0).WithMessage("Ümumi xərc müsbət olmalıdır.");

            RuleFor(x => x.InsuranceCoverage)
                .LessThanOrEqualTo(x => x.TotalCost).WithMessage("Sığorta məbləği ümumi xərci keçə bilməz.");
        }

    }
}
