using Microsoft.AspNetCore.Builder;

namespace WebApplication1.Filters
{
    public static class LogHttpRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseElasticSearchRequestLogging(this IApplicationBuilder builder) {
            return builder.UseMiddleware<LogHttpRequestMiddleware>();
        }
    }
}