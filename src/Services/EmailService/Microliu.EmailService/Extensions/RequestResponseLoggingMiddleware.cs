using Microliu.Core.Loggers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Microliu.EmailService.API.Extensions
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        private SortedDictionary<string, object> _data;
        private Stopwatch _stopwatch;


        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().StartsWith("/hangfire") ||
               context.Request.Path.ToString().StartsWith("/cap") ||
               context.Request.Path.ToString().ToLower().StartsWith("/images"))
            {
                await _next(context);
            }
            else
            {
                _stopwatch.Restart();
                _data = new SortedDictionary<string, object>();

                HttpRequest request = context.Request;
                _data.Add("request.url", request.Path.ToString());
                _data.Add("request.headers", request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())));
                _data.Add("request.method", request.Method);
                _data.Add("request.executeStartTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                // 获取请求body内容
                if (request.Method.ToLower().Equals("post"))
                {
                    // 启用倒带功能，就可以让 Request.Body 可以再次读取
                    request.EnableRewind();

                    Stream stream = request.Body;
                    byte[] buffer = new byte[request.ContentLength.Value];
                    stream.Read(buffer, 0, buffer.Length);
                    if (!_data.ContainsKey("request.body"))
                        _data.Add("request.body", Encoding.UTF8.GetString(buffer));

                    request.Body.Position = 0;
                }
                else if (request.Method.ToLower().Equals("get"))
                {
                    if (!_data.ContainsKey("request.body"))
                        _data.Add("request.body", request.QueryString.Value);
                }

                // 获取Response.Body内容
                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);
                    if (!_data.ContainsKey("response.body"))
                        _data.Add("response.body", await GetResponse(context.Response));
                    if (!_data.ContainsKey("response.executeEndTime"))
                        _data.Add("response.executeEndTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    await responseBody.CopyToAsync(originalBodyStream);
                }

                // 响应完成记录时间和存入日志
                context.Response.OnCompleted(() =>
                {
                    _stopwatch.Stop();
                    try
                    {
                        if (!_data.ContainsKey("elaspedTime"))
                            _data.Add("elaspedTime", _stopwatch.ElapsedMilliseconds + "ms");

                    }
                    catch
                    {
                        if (!_data.ContainsKey("elaspedTime"))
                            _data.Add("elaspedTime", "0ms");
                    }

                    string tag = "page";
                    string json = "";
                    if (request.Path.ToString().StartsWith("/papi/v"))
                    {
                        tag = "api";
                        json = JsonConvert.SerializeObject(_data);
                    }

                    _logger.TraceBuilder(json)
                            .AddTags("http", tag, request.Method.ToUpper())
                            .AddObject(json)
                            .Submit();
                    return Task.CompletedTask;
                });

            }
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }

    /// <summary>
    /// 扩展中间件
    /// </summary>
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
