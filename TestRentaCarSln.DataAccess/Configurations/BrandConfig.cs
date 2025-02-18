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
    public class BrandConfig : BaseEntityConfig<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Model).HasMaxLength(100);
            builder.Property(x =>x.Name).HasMaxLength(100);

            base.Configure(builder);
        }

    }
}
