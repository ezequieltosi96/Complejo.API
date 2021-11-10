using Microsoft.AspNetCore.Builder;

namespace Complejo.API.Middlewares
{
    public static class MiddlewareRegistration
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder builder)
        {
            // ---- register custom middlewares ----
            builder.UseMiddleware<ExceptionMiddleware>();

            return builder;
        }
    }
}
