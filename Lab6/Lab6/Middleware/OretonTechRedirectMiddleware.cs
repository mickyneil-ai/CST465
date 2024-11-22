using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Lab6.Middleware
{
    public class OretonTechRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        public OretonTechRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Set the response status code to 302 (Found)
            context.Response.StatusCode = StatusCodes.Status302Found;

            // Add a "Location" header with the redirect URL
            context.Response.Headers["Location"] = "https://www.oit.edu";

            // Don't call _next(context) to short-circuit the pipeline
            await Task.CompletedTask;
        }
    }
}
