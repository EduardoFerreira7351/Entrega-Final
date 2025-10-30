// FastPoint/FastPoint.API/Middlewares/GlobalExceptionHandlerMiddleware.cs
using System.Net;
using System.Text.Json;

namespace FastPoint.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Tenta executar a próxima etapa do pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se ocorrer uma exceção, captura e trata
                _logger.LogError(ex, "Ocorreu uma exceção não tratada: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Define o status code (500 para erro interno)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Cria um objeto de resposta padronizado
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro interno no servidor. Tente novamente mais tarde.",
                // (Opcional: Apenas em desenvolvimento)
                // Detalhes = exception.Message 
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}