using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class InsuranceConfig : BaseEntityConfig<Insurance>
    {
        public override void Configure(EntityTypeBuilder<Insurance> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Price).HasPrecision(7, 2);
            builder.Property(x => x.PolicyName).HasMaxLength(100);

        }

    }
}
