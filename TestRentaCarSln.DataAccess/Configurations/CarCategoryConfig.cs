using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarSln.DataAccess.Configurations.Base;

namespace TestRentaCarDataAccess.Configurations
{
    public class CarCategoryConfig : BaseEntityConfig<Entities.CarCategory>
    {
        public override void Configure(EntityTypeBuilder<Entities.CarCategory> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.HasData(new Entities.CarCategory[]
            {
                new Entities.CarCategory()
                {
                    Id = (int)Enums.CarCatagory.Sedan,
                    Name = Enums.CarCatagory.Sedan.ToString(),
                    CreatedDate = DateTime.UtcNow
                },
                new Entities.CarCategory()
                {
                    Id = (int)Enums.CarCatagory.Suv,
                    Name = Enums.CarCatagory.Suv.ToString(),
                    CreatedDate = DateTime.UtcNow
                },
                new Entities.CarCategory()
                {
                    Id = (int)Enums.CarCatagory.Sport,
                    Name = Enums.CarCatagory.Sport.ToString(),
                    CreatedDate = DateTime.UtcNow
                },
                new Entities.CarCategory()
                {
                    Id = (int)Enums.CarCatagory.Coupe,
                    Name = Enums.CarCatagory.Coupe.ToString(),
                    CreatedDate = DateTime.UtcNow
                }

            });

        }

    }
}
