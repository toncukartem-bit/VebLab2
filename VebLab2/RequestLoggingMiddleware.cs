using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using System.Threading.Tasks;

namespace VebLab2
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //IP і URL 
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Невідомий IP";
            var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";

            //повідомлення в Serilog
            _logger.LogInformation("IP: {Ip} | URL: {Url}", ip, url);

            await _next(context);
        }
    }
}