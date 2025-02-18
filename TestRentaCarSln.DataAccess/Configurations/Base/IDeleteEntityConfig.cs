using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Configurations.Base
{
    public class IDeleteEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IDeleteEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasQueryFilter(x=>x.IsDeleted == false);
            builder.Property(x => x.DeletedDate);
        }
    }
}
