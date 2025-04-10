using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class BranchConfig : BaseEntityConfig<Branch>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(100);

            builder.HasMany(x => x.Cars)
                .WithOne(x => x.Branch)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

    }
}
