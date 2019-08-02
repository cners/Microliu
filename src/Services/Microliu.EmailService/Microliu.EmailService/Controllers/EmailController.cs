using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Microliu.EmailService.Controllers
{
    //[Authorize]//添加 Authorize Attribute 以使该控制器启用认证
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [Authorize]
        [HttpGet("send")]
        public string Send()
        {
            string result = "[" +
                            HttpContext.Connection.LocalIpAddress.MapToIPv4() + ":" +
                            HttpContext.Connection.LocalPort.ToString() + "] " +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "发送邮件";
            return result;
        }

        [HttpGet(nameof(GetEmail))]
        public string GetEmail()
        {
            var emails = "This is a emails";
            return emails;
        }


    }
}