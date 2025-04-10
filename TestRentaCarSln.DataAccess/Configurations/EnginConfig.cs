using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class EnginConfig : BaseEntityConfig<Engine>
    {
        public override void Configure(EntityTypeBuilder<Engine> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.EnginType).HasMaxLength(100);

            builder.Property(x => x.EnginCapacity).HasPrecision(7, 2);

        }

    }
}
