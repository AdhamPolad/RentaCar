﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarDataAccess.Enums;

namespace TestRentaCarDataAccess.Configurations
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {

        }
    }
}
