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
    public class BaseEntityConfig<T> : IDeleteEntityConfig<T> where T : BaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);
        }
    }
}
