using Microliu.Core.RedisCache;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ptibro.Partner.API.Extensions
{
    /// <summary>
    ///  Http 请求中间件
    /// </summary>
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microliu.Core.Loggers.ILogger _logger;
        private Dictionary<string, object> _data;

        private Stopwatch _stopwatch;

        /// <summary>
        /// 构造 Http 请求中间件
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="cacheService"></param>
        public HttpContextMiddleware(
            RequestDelegate next,
            Microliu.Core.Loggers.ILogger logger,
            ICacheService cacheService
            )
        {
            _next = next;
            _logger = logger;
        }


        /// <summary>
        /// 执行响应流指向新对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            _data = new Dictionary<string, object>();
            if (!context.Request.Path.ToString().ToLower().StartsWith("/papi"))
            {
                await this._next(context);

                context.Response.OnCompleted(() =>
                {
                    return Task.CompletedTask;
                });
            }
            else
            {
                //context.Request.EnableRewind();
                //context.Request.EnableBuffering();
                _stopwatch = new Stopwatch();
                _stopwatch.Start();


                _data.Add("request.method", context.Request.Method);
                _data.Add("request.query.string", context.Request.QueryString.Value);
                _data.Add("request.path", context.Request.Path);
                _data.Add("request.ip", context.Connection.RemoteIpAddress.ToString());

                if (context.Request.QueryString.Value.Contains("%"))
                {
                    _data["request.query.string"] = System.Web.HttpUtility.UrlDecode(context.Request.QueryString.Value);
                }

                var reqOrigin = context.Request.Body;
                var resOrigin = context.Response.Body;
                try
                {
                    using (var newReq = new MemoryStream())
                    {
                        //替换request流
                        context.Request.Body = newReq;
                        using (var newRes = new MemoryStream())
                        {
                            //替换response流
                            context.Response.Body = newRes;
                            using (var reader = new StreamReader(reqOrigin, Encoding.UTF8))
                            {
                                //读取原始请求流的内容
                                _data.Add("request.body", reader.ReadToEnd());
                            }
                            using (var writer = new StreamWriter(newReq, Encoding.UTF8))
                            {
                                writer.Write(_data["request.body"].ToString());
                                writer.Flush();
                                newReq.Position = 0;
                                await _next(context);
                            }


                            string tempRb = "";
                            using (var reader = new StreamReader(newRes, Encoding.UTF8))
                            {
                                newRes.Position = 0;
                                tempRb = reader.ReadToEnd();
                                _data.Add("response.body", tempRb);
                            }
                            using (var writer = new StreamWriter(resOrigin, Encoding.UTF8))
                            {
                                writer.Write(tempRb);
                            }
                        }
                    }
                }
                finally
                {
                    context.Request.Body = reqOrigin;
                    context.Response.Body = resOrigin;
                }

                // 响应完成时存入缓存
                context.Response.OnCompleted(() =>
                {
                    _stopwatch.Stop();
                    _data.Add("request.to.response", _stopwatch.ElapsedMilliseconds + "ms");
                    var json = JsonConvert.SerializeObject(_data);
                    _logger.Trace(json, "api");

                    return Task.CompletedTask;
                });

            }
        }
    }
}
