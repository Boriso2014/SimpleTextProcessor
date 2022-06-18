using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace SimpleTextProcessor.Infrastructure.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Request failed:{NewLine}{Url}{NewLine}{Exception}",
                    Environment.NewLine, context.Request.GetDisplayUrl(), Environment.NewLine, ex);

                if (_env.IsDevelopment())
                {
                    throw;
                }
                else
                {
                    await HandleAsync(context, ex);
                }
            }
        }

        private async Task HandleAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.ToString(),
                Details = ex.Message
            }.ToString());
        }
    }
}
