using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Web.API.Exceptions;

namespace Web.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ResourceNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await SendResponse(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await SendResponse(context, StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        private static async Task SendResponse(HttpContext context, int statusCode, string message)
        {
            await SendResponse(context, statusCode, new { Messages = new[] { message } });
        }

        private static async Task SendResponse(HttpContext context, int statusCode, object result)
        {
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
