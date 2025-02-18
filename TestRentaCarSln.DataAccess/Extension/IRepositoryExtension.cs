using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarSln.DataAccess.Extension
{
    public static class IRepositoryExtension
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));

            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

    }
}
