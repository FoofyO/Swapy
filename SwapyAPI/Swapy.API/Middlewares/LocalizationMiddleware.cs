using Swapy.Common.Attributes;
using Swapy.Common.Enums;

namespace Swapy.API.Middlewares
{
    public class LocalizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            bool shouldLocalize = context.GetEndpoint()?.Metadata?.Any(em => em.GetType() == typeof(LocalizeAttribute)) ?? false;

            if (shouldLocalize)
            {
                string localizationValue = context.Request.Headers["Localization"];
                Languages language;

                switch (localizationValue?.ToLower())
                {
                    case "en":
                        language = Languages.English;
                        break;
                    case "ru":
                        language = Languages.Russian;
                        break;
                    case "az":
                        language = Languages.Azerbaijani;
                        break;
                    default:
                        language = Languages.English;
                        break;
                }

                context.Items["Language"] = language;
            }

            await next(context);
        }
    }
}
