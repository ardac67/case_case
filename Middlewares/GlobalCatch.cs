using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class GlobalCatchMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalCatchMiddleware> _logger;

    public GlobalCatchMiddleware(RequestDelegate next, ILogger<GlobalCatchMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError($"Something happened i dont know what that is: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error.",
            Detailed = exception.Message
        };

        return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
}
