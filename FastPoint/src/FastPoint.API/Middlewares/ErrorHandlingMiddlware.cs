using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FastPoint.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await WriteJsonAsync(context, new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "NotFound");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await WriteJsonAsync(context, new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "BadRequest");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await WriteJsonAsync(context, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await WriteJsonAsync(context, new { error = "Ocorreu um erro interno no servidor." });
            }
        }

        private static async Task WriteJsonAsync(HttpContext context, object obj)
        {
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(obj);
            await context.Response.WriteAsync(json);
        }
    }
}
