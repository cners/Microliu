using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Utils.jwt
{
    public class UseJwtMiddleware
    {
        private readonly RequestDelegate _next;
        private JwtOptions _jwtOptions = new JwtOptions();
        private IJwt _jwt;
        public UseJwtMiddleware(RequestDelegate next, IConfiguration configration, IJwt jwt)
        {
            _next = next;
            this._jwt = jwt;
            configration.GetSection("Jwt").Bind(_jwtOptions);
        }
        public Task InvokeAsync(HttpContext context)
        {
            if (_jwtOptions.Enabled == false)
            {
                return this._next(context);
            }
            if (context.Request.Path.ToString().StartsWith("/hangfire") ||
                 context.Request.Path.ToString().StartsWith("/cap") ||
                 context.Request.Path.ToString().ToLower().StartsWith("/images") ||
                 context.Request.Path.ToString().ToLower().StartsWith("/pimg"))
            {
                return _next(context);
            }

            var requestUrl = context.Request.Path.ToString().ToLower();
            // 匹配已忽略的路由，支持模糊匹配，模糊匹配目前仅支持结尾为*
            var isNoAuthUrl = _jwtOptions.IgnoreUrls.Select(x =>
            {
                if (x.ToLower() == requestUrl) return x;
                //   /papi/partner/18323121/1231231
                //   /papi/partner/*      
                if (x.ToLower().EndsWith("*"))
                {
                    if (requestUrl.Where(r => r == '/').Count() == x.ToLower().Where(r => r == '/').Count() &&
                        requestUrl.Substring(0, x.ToLower().TrimEnd('*').Length) == x.ToLower().TrimEnd('*'))
                    {
                        return x;
                    }
                }
                return null;
            }).Where(x => x != null);
            if (isNoAuthUrl.Count() > 0)
            {
                return this._next(context);
            }
            else
            {
                if (context.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authValue))
                {
                    var authstr = authValue.ToString();
                    var prefix = "Bearer";
                    if (prefix.Length > 0)
                    {
                        authstr = authValue.ToString().Substring(prefix.Length + 1, authValue.ToString().Length - (prefix.Length + 1));
                    }
                    if (this._jwt.ValidateToken(authstr, out Dictionary<string, string> Clims))
                    {
                        foreach (var item in Clims)
                        {
                            context.Items.Add(item.Key, item.Value);
                        }
                        return this._next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        ReturnResult returnResult = new ReturnResult();
                        returnResult.SetSuccess(false).SetMessage("auth vaild fail");
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(returnResult), Encoding.UTF8);
                    }
                }
                else
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    ReturnResult returnResult = new ReturnResult();
                    returnResult.SetSuccess(false).SetMessage("auth vaild fail");
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(returnResult), Encoding.UTF8);
                }
            }
        }
    }

    public static class UseUseJwtMiddlewareExtensions
    {
        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="builder">
        /// <returns></returns>
        public static IApplicationBuilder UseJwt(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseJwtMiddleware>();
        }
    }
}
