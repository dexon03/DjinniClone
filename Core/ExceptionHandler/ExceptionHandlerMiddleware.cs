using System.Net;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Core.ExceptionHandler;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string ContentType = "application/json";
    private readonly ILogger _logger = Log.ForContext<ExceptionHandlerMiddleware>();

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            await HandleException(httpContext, exception);
        }
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = ContentType;
        switch (exception)
        {
            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case ExceptionWithStatusCode exceptionWithStatusCode:
                response.StatusCode = (int)exceptionWithStatusCode.StatusCode;
                break; 
            case ValidationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        
        _logger.Error(exception, exception.Message);
        var result = JsonConvert.SerializeObject(new { message = exception?.Message });
        return response.WriteAsync(result);
    }
}