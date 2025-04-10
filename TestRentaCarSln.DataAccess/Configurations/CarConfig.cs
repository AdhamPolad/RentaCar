using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarSln.DataAccess.Configurations.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.DataAccess.Configurations
{
    public class CarConfig : BaseEntityConfig<Car>
    {
        public override void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(x => x.PricePerDay).HasPrecision(7, 2);
            builder.HasOne(x => x.Model)
                   .WithMany(x => x.Cars)
                   .HasForeignKey(x=>x.ModelId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.CarCatagory)
                   .WithMany(cc => cc.Cars)
                   .HasForeignKey(c => c.CarCatagoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CarDetails)
                   .WithOne(x => x.Car)
                   .HasForeignKey<Car>(x => x.CarDetailId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(c => c.Insurance)
                   .WithOne(i => i.Car)
                   .HasForeignKey<Car>(c => c.InsuranceId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.LicensePlate)
                   .HasMaxLength(100);

            base.Configure(builder);
        }

    }
}
