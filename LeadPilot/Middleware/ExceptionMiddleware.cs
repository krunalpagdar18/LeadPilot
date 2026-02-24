using LeadPilot.ViewModels;
using Serilog;

namespace LeadPilot.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception occurred");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new ResponseViewModel<Exception>("Internal server error",null);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
