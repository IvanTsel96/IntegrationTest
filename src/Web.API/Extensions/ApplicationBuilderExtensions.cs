using Microsoft.AspNetCore.Builder;
using Web.API.Middlewares;

namespace Web.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
