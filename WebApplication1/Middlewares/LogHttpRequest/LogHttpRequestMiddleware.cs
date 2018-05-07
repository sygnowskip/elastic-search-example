using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using NLog;

namespace WebApplication1.Filters
{
    public class LogHttpRequestMiddleware
    {
        private readonly Logger _logger = LogManager.GetLogger("HttpRequestLogger");
        private readonly RequestDelegate _next;

        public LogHttpRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                Log(context);
            }
            catch (Exception ex)
            {
                Log(context, ex);
                throw;
            }
        }

        private void Log(HttpContext context, Exception exception = null)
        {
            var httpRequestEvent = new LogEventInfo(LogLevel.Debug, _logger.Name, "Incoming request");
            httpRequestEvent.Properties["Url"] = context.Request.GetDisplayUrl();
            httpRequestEvent.Properties["Method"] = context.Request.Method;
            httpRequestEvent.Properties["Status"] = exception != null ? (int)HttpStatusCode.InternalServerError : context.Response.StatusCode;
            
            _logger.Log(httpRequestEvent);
        }
    }
}