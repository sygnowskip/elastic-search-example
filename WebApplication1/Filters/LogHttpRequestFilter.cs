using System;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using LogLevel = NLog.LogLevel;

namespace WebApplication1.Filters
{
    public class LogHttpRequestFilter : ActionFilterAttribute
    {
        private readonly Logger _logger = LogManager.GetLogger("HttpRequestLogger");
        
        public override void OnActionExecuted(ActionExecutedContext context)
        {   
            var requestContext = context.HttpContext.Request;
            var responseContext = context.HttpContext.Response;
            
            var httpRequestEvent = new LogEventInfo(LogLevel.Debug, _logger.Name, "Incoming request");
            httpRequestEvent.Properties["Url"] = requestContext.GetDisplayUrl();
            httpRequestEvent.Properties["Method"] = requestContext.Method;
            httpRequestEvent.Properties["Status"] = context.Exception != null ? (int)HttpStatusCode.InternalServerError : responseContext.StatusCode;
            
            _logger.Log(httpRequestEvent);
            
            base.OnActionExecuted(context);
        }
    }
}