using Microliu.Core.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.SMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly SMS _sms;
        private readonly ILogger _logger;

        public SMSController(SMS sms, ILogger logger)
        {
            _sms = sms;
            _logger = logger;
            _logger.Trace("进入构造函数 SMSController ");
        }

        [HttpGet(nameof(Send))]
        public string Send()
        {
            _logger.Error("调用了 api/sms/send 接口");
            return "You send one sms!";
        }

        [HttpGet(nameof(SendMessage))]
        public string SendMessage(string message)
        {
            //Console.WriteLine(message);
            //return "发送了：" + message;
            return _sms.SendAsync(message).Result;
        }
        /// <summary>
        /// 
        /// Get Token
        /// http://192.168.30.123:9400/sms/api/session/login  
        /// POST {"name":"liuzhuang","password":"liuzhuang"}
        /// 
        /// Request
        /// POST http://192.168.30.123:9400/sms/api/sms/authsend
        /// Bearer Token=Token Value
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(AuthSend))]
        [Authorize]
        public string AuthSend()
        {
            var headers = HttpContext.Request.Headers;
            _logger.Error("调用了 api/sms/AuthSend 接口");
            return "AuthSend!";
        }

    }
}