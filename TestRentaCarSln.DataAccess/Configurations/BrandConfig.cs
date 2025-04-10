using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarSln.DataAccess.Configurations.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.DataAccess.Configurations
{
    public class BrandConfig : BaseEntityConfig<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x =>x.Name).HasMaxLength(100);

            base.Configure(builder);
        }

    }
}
