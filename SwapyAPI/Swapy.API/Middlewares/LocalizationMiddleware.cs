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
                Language language;

                switch (localizationValue?.ToLower())
                {
                    case "en":
                        language = Language.English;
                        break;
                    case "ru":
                        language = Language.Russian;
                        break;
                    case "az":
                        language = Language.Azerbaijani;
                        break;
                    default:
                        language = Language.English;
                        break;
                }

                context.Items["Language"] = language;
            }

            await next(context);
        }
    }
}
