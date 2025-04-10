using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class DiscountCustomerConfig : BaseEntityConfig<DiscountCustomer>
    {
        public override void Configure(EntityTypeBuilder<DiscountCustomer> builder)
        {
            base.Configure(builder);

            builder.HasKey(dc => new {dc.CustomerId, dc.DiscountId});

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.DiscountCustomers)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Discount)
                   .WithMany(x=>x.DiscountCustomers)
                   .HasForeignKey(x=>x.DiscountId)
                   .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
