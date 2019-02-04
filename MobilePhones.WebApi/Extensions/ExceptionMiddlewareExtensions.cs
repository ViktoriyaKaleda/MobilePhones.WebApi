using Microsoft.AspNetCore.Builder;
using MobilePhones.WebApi.Middlewares;

namespace MobilePhones.WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
