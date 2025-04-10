using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using System.Text.Json;
using TestRentaCar.Api.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Api.Extensions
{
    public class ExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception exp)
			{

				await HandleException(context, exp);
			}
        }

        private async Task HandleException(HttpContext context, Exception exp)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            GenericResponceModel<ErrorDetail> model = new()
            {
                Data = new()
                {
                    Message = exp.Message,
                },
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = new List<string>() { "Error happened" }
            };

            string content = JsonSerializer.Serialize(model);

            await context.Response.WriteAsync(content);
        }
    }

}

