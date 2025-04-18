
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BookingSystem.Application.Exceptions;

namespace BookingSystem.API.ExceptionMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    // ExceptionMiddleware - 
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { message = ex.Message });
            await context.Response.WriteAsync(result);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new 
                { error = ex.Message, 
                    details = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                    
                });
            await context.Response.WriteAsync(result);
        }
    }
}

