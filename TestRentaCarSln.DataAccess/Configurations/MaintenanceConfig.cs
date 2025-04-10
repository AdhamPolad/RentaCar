using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class MaintenanceConfig : BaseEntityConfig<Maintenance>
    {
        public override void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Rental)
                   .WithMany(x => x.Maintenances)
                   .HasForeignKey(x => x.RentalId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.Property(x => x.MaintenanceDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.TotalCost)
                   .HasPrecision(7, 2);

            builder.Property(x => x.InsuranceCoverage)
                   .HasPrecision(7, 2);
        }

    }
}
