using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    class CarDetailsConfig : BaseEntityConfig<CarDetails>
    {
        public override void Configure(EntityTypeBuilder<CarDetails> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Transmision)
                   .HasConversion<string>();

            builder.HasOne(x => x.Engine)
                   .WithMany(x => x.CarDetails)
                   .HasForeignKey(x => x.EngineId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.Property(x => x.Color)
                   .HasMaxLength(100);

        }

    }
}
