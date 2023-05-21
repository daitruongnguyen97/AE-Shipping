using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shipping.WebApi.Swagger;

public class FileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation?.OperationId?.ToLower().Contains("upload") ?? false)
            operation.RequestBody = new OpenApiRequestBody
            {
                Description = "upload file ",
                Content = new Dictionary<String, OpenApiMediaType>
                        {
                            {
                                "multipart/form-data", new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Required = new HashSet<String>{ "file" },
                                        Properties = new Dictionary<String, OpenApiSchema>
                                        {
                                            {
                                                "files", new OpenApiSchema()
                                                {
                                                    Type = "string",
                                                    Format = "binary"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
            };
    }
}