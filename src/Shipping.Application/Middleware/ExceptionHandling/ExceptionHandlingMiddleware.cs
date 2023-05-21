

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shipping.Application.Extensions;

namespace Shipping.Application.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    private readonly ILogger logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILoggerFactory loggerFactory
    )
    {
        this.next = next;
        logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
    }

    public async Task Invoke(HttpContext context /* other scoped dependencies */)
    {
        try
        {
            await next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex).ConfigureAwait(false);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var metadata = Array.Empty<string>();

        logger.LogError(exception, exception.Message);
        Console.WriteLine("ERROR:" + exception.Message + exception.StackTrace);

        if(exception.InnerException != null)
            Console.WriteLine("INNER DETAILS:" + exception.InnerException.Message + exception.InnerException.StackTrace);

        if (exception is ArgumentRequiredException)
        {
            metadata = (exception as ArgumentRequiredException).ArgRequired.ToArray();
        }

        var codeInfo = ExceptionToHttpStatusMapper.Map(exception);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)codeInfo.HttpStatusCode switch
        {
            int code when code >= 500 => (int)HttpStatusCode.BadRequest, //not found error
            _ => (int)codeInfo.HttpStatusCode //unhandler error
        };

        var result = string.Empty;
        if (metadata.Any()) {
            result = JsonConvert.SerializeObject(new HttpExceptionWrapperWithMetaData(context.Response.StatusCode, codeInfo.Message, metadata));
        } 
        else
        {
            result = JsonConvert.SerializeObject(new HttpExceptionWrapper(context.Response.StatusCode, codeInfo.Message));
        }

        return context.Response.WriteAsync(result);
    }
}

public class HttpExceptionWrapperWithMetaData
{
    public int statusCode { get; }

    public string message { get; }

    public string[] metadata { get; }

    public HttpExceptionWrapperWithMetaData(int code, string error, string[] metadata)
    {
        statusCode = code;
        message = error;
        this.metadata = metadata;
    }
}

public class HttpExceptionWrapper
{
    public int statusCode { get; }

    public string message { get; }

    public HttpExceptionWrapper(int code, string error)
    {
        statusCode = code;
        message = error;
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(
        this IApplicationBuilder app,
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        Func<Exception, HttpStatusCode>? customMap = null
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    )
    {
        ExceptionToHttpStatusMapper.CustomMap = customMap;
        return app
            .UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
