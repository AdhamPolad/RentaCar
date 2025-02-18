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
    public class CustomerConfig : BaseEntityConfig<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Address)
                   .HasMaxLength(100);
            builder.Property(x =>x.DriverLisenceNumber)
                   .HasMaxLength(100);
            builder.Property(x=>x.FullName)
                   .HasMaxLength(100);
            builder.Property(x=>x.Email)
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Email)
                   .IsUnique();
            
            builder.Property(x=>x.PhoneNumber)
                   .HasMaxLength(100);

        }

    }
}
