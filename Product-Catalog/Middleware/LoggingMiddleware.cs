using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Product_Catalog.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation(
                "Incoming Request: {Method} {Path}\nHeaders: {@Headers}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.Request.Headers
            );
            await _next(httpContext);

            _logger.LogInformation(
                "Outgoing Response: {StatusCode}\nHeaders: {@Headers}",
                httpContext.Response.StatusCode,
                httpContext.Response.Headers
            );
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
