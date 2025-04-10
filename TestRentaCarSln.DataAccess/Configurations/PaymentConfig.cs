using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Configurations.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.DataAccess.Configurations
{
    public class PaymentConfig : BaseEntityConfig<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Status)
                   .HasDefaultValue("Pending");

            builder.HasOne(x => x.Rental)
                   .WithOne(x => x.Payment)
                   .HasForeignKey<Payment>(x => x.RentalId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Amount)
                   .HasPrecision(7, 2);

            builder.Property(x=>x.PaymentDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Status)
                   .HasMaxLength(100);

            builder.Property(x => x.PaymentMethod)
                   .HasMaxLength(100);

        }

    }
}
