using Swapy.API.Middlewares;

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
