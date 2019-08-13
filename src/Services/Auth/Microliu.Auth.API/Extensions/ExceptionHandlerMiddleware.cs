using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microliu.Auth.API.Extensions
{//没用到
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            // 记录日志
            Console.WriteLine("Error : Exception - " + exception.Message);


            var response = context.Response;

            // 友好提示
            if (exception is UnauthorizedAccessException)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is Exception)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            response.ContentType = context.Request.Headers["Accept"];

            var result = new ReturnResult(response.StatusCode, "权限服务异常，请稍后再试");
            if (response.ContentType.ToLower().Equals("application/xml"))
            {

                await response.WriteAsync(Object2XmlString(result));
            }
            else
            {
                response.ContentType = "application/json";
                await response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 对象转为Xml
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string Object2XmlString(object o)
        {
            StringWriter sw = new StringWriter();
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(sw, o);
            }
            catch
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Dispose();
            }
            return sw.ToString();
        }
    }
}
