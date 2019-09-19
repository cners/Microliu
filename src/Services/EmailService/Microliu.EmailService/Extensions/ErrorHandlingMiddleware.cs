using Microliu.Core.Loggers;
using Microliu.EmailService.API.Models;
using Microliu.EmailService.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.EmailService.API.Extensions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpResponse response = context.Response;
            try
            {
                await next(context);
            }
            catch (EmailException emailException)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ContentType = "application/json;charset=utf-8";
                var result = new ReturnResult(false, emailException.Message);
                result.Data = string.Empty;
                await HandleExceptionAsync(response, result);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ContentType = "application/json;charset=utf-8";
                var result = new ReturnResult(false, "接口异常，请稍后再试");
                result.Data = "";
                _logger.ErrorBuilder($"[{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [接口异常] [{ex.Message}]")
                       .SetException(ex)
                       .AddTags("error", "exception")
                       .AddObject(result)
                       .Submit();

                await HandleExceptionAsync(response, result);
            }
        }

        private static async Task HandleExceptionAsync(HttpResponse response, ReturnResult result)
        {
            await response.WriteAsync(JsonConvert.SerializeObject(result), Encoding.UTF8);//.ConfigureAwait(false);
        }

    }
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
