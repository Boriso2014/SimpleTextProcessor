using Microsoft.AspNetCore.Builder;

namespace SimpleTextProcessor.Services.Middleware
{
    public static class GlobalExceptionHandlerRegistration
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
