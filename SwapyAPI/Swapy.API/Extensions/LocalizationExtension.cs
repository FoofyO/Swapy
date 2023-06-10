using Swapy.API.Middleware;

namespace Swapy.API.Extensions
{
    public static class LocalizationExtension
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
