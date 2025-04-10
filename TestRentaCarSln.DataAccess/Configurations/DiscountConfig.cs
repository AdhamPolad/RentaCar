using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class DiscountConfig : BaseEntityConfig<Discount>
    {
        public override void Configure(EntityTypeBuilder<Discount> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code).HasMaxLength(200);

            builder.Property(x => x.DiscountAmount)
                .HasPrecision(7, 2);



        }

    }
}
