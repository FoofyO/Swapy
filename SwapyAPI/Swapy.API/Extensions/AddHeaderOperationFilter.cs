using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swapy.API.Extensions
{
    public class AddHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Localization",
                In = ParameterLocation.Header,
                Description = "Localization header",
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
