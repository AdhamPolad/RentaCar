using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Configurations.Base
{
    public class ICreateEntityConfig<T> : IEntityTypeConfiguration<T> where T: class,ICreateEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedDate);
        }
    }
}
