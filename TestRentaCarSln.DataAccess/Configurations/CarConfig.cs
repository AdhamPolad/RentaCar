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
    public class CarConfig : BaseEntityConfig<Car>
    {
        public override void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(x => x.PricePerDay).HasPrecision(7, 2);
            builder.HasOne(x => x.Brand)
                   .WithMany(x => x.Car)
                   .HasForeignKey(x=>x.BrandId)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }

    }
}
