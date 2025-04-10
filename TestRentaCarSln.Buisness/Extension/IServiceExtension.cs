using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Abstractions.Infrastructure;
using TestRentaCar.Buisness.Implementations;
using TestRentaCar.Buisness.Implementations.Infrastructure;
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
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IInsuranceService, InsuranceSevice>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
        }

    }
}
