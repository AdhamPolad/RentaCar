using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class ReviewConfig : BaseEntityConfig<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Comment).HasMaxLength(100);

            builder.HasOne(x => x.Car)
                   .WithMany(x => x.Reviews)
                   .HasForeignKey(x => x.CarId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
                   .WithMany(x=>x.Reviews)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
