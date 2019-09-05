using Microliu.EmailService.API.Models;
using Microliu.EmailService.Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Microliu.EmailService.API.Extensions
{
    /// <summary>
    /// 全局异常中间件
    /// </summary>
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 返回友好的提示
            HttpResponse response = context.Response;

            try
            {
                await next(context);
            }
            catch (EmailException aex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ContentType = "application/json";
                response.ContentType = context.Request.Headers["Accept"];
                var result = new ReturnResult(response.StatusCode, aex.Message);
                result.Data = string.Empty;
                await HandleExceptionAsync(response, result);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ContentType = "application/json";
                response.ContentType = context.Request.Headers["Accept"];
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
}
