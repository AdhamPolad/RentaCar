using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Abstractions;
using TestRentaCarSln.Buisness.Implementations;
using TestRentaCarSln.DataAccess.Extension;

namespace TestRentaCarSln.Buisness.Extension
{
    public static class IServiceExtension 
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            IRepositoryExtension.AddRepository(services, configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICarService, CarService>();
        }

    }
}
