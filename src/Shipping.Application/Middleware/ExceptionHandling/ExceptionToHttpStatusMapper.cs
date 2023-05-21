using System.Net;

namespace Shipping.Application.Middleware;
public class HttpStatusCodeInfo
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public string Code { get; }
    public string Message { get; }

    public HttpStatusCodeInfo(HttpStatusCode httpStatusCode, string code, string message)
    {
        HttpStatusCode = httpStatusCode;
        Code = code;
        Message = message;
    }
}

public static class ExceptionToHttpStatusMapper
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public static Func<Exception, HttpStatusCode>? CustomMap { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    public static HttpStatusCodeInfo Map(Exception exception)
    {
        var code = exception switch
        {
            UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
            NotImplementedException _ => HttpStatusCode.NotImplemented,
            InvalidOperationException _ => HttpStatusCode.Conflict,
            ArgumentException _ => HttpStatusCode.BadRequest,
            System.ComponentModel.DataAnnotations.ValidationException _ => HttpStatusCode.BadRequest,
            FluentValidation.ValidationException _ => HttpStatusCode.BadRequest,
            _ => CustomMap?.Invoke(exception) ?? HttpStatusCode.InternalServerError
        };

        return new HttpStatusCodeInfo(code, "", GetAllErrorMessage(exception));
    }

    public static string GetAllErrorMessage(Exception ex)
    {
        var message = ex?.Message;
        var exception = ex?.InnerException;
        while (exception != null)
        {
            message += Environment.NewLine + exception.Message;
            exception = exception.InnerException;
        }
        return message;
    }

}
