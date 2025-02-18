using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCarSln.Buisness.Extension
{
    public static class CustomValidationResponse
    {
        public static void UseCustomeValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x=>x.Errors.Count() > 0)
                                        .SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage);

                    ErrorResponseDto errorResponseDto = new ErrorResponseDto(errors.ToList());

                    var response = new GenericResponceModel<ErrorResponseDto> { Data = errorResponseDto , StatusCode = 400};
                    return new BadRequestObjectResult(response);

                };

            });

        }

    }



}
