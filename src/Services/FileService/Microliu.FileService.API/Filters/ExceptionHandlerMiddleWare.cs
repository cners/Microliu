﻿using Microliu.FileService.Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microliu.FileService.API
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
            catch (FileServiceException aex)
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
