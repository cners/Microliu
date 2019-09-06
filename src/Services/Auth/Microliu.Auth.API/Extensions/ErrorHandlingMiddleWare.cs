using Microliu.Auth.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Microliu.Auth.API.Extensions
{
    public class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpResponse response = context.Response;

            try
            {
                await next(context);
            }
            catch (AuthException authException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ContentType = "application/json;charset=utf-8";
                var result = new ReturnResult(response.StatusCode, authException.Message);
                result.Data = string.Empty;
                await HandleExceptionAsync(response, result);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ContentType = "application/json;charset=utf-8";
                var result = new ReturnResult(response.StatusCode, "AuthService异常，请稍后再试");
                result.Data = ex.Message;
                await HandleExceptionAsync(response, result);
            }
        }

        private static async Task HandleExceptionAsync(HttpResponse response, ReturnResult result)
        {
            await response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
        }
    }

    public static class ErrorHandlingExtension
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleWare>();
        }
    }

}
