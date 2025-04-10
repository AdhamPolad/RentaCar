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
    public class RentalConfig : BaseEntityConfig<Rental>
    {
        public override void Configure(EntityTypeBuilder<Rental> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Status)
                .HasDefaultValue("Active");

            builder.HasOne(x => x.Car)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x=>x.Customer)
                   .WithMany(x=>x.Rental)
                   .HasForeignKey(x=>x.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.RentalDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.TotalPrice)
                .HasPrecision(7, 2);

            builder.Property(x => x.Status)
                   .HasMaxLength(100);

            builder.Property(x => x.DiscountAmount)
                   .HasPrecision(7, 2);

            builder.Property(x=>x.PenaltyAmount)
                   .HasPrecision(7, 2);
        }

    }
}
