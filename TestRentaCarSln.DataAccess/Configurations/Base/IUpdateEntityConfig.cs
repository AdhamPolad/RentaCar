using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Configurations.Base
{
    public class IUpdateEntityConfig<T> : ICreateEntityConfig<T> where T : class, IUpdateEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedDate);
            base.Configure(builder);
        }

    }
}
