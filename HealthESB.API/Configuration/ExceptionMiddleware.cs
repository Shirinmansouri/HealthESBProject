using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HealthESB.Domain.Model;
using HealthESB.Framework.Utility;

namespace HealthESB.API.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n {ex.StackTrace} ");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HealthESBApiResponseCode.ApiError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                ResponseCode = context.Response.StatusCode.ToString(),
                Description = HealthESBApiResponseMessages.ApiError
            }.ToString());
        }
    }
}
