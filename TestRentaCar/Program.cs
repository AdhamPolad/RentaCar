using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Context;
using TestRentaCar.Api.Extensions;
using TestRentaCar.Buisness.Validations.Car;
using TestRentaCarSln.Buisness.Extension;

namespace TestRentaCar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<PostCarDtoValidator>());
            builder.Services.AddJsonEnumStringEnumConverter();
            builder.Services.UseCustomeValidationResponse();

            var logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(builder.Configuration)
                  .Enrich.FromLogContext()
                  .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddAuth(builder.Configuration);
            builder.Services.AddScoped<ExceptionHandler>();
            builder.Services.AddSwagerAuth();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandler>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var userName = context.User?.Identity.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("User_Name", userName);
                await next(context);

            });

            app.MapControllers();

            app.Run();
        }
    }
}
