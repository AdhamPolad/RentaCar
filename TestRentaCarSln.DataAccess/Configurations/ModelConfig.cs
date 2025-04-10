using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class ModelConfig : BaseEntityConfig<TestRentaCarDataAccess.Entities.Model>
    {
        public override void Configure(EntityTypeBuilder<TestRentaCarDataAccess.Entities.Model> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.HasOne(m => m.Brand)
                   .WithMany(b => b.Models)
                   .HasForeignKey(m => m.BrandId)
                   .OnDelete(DeleteBehavior.NoAction);


        }

    }
}
