using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.SMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        [HttpGet(nameof(Send))]
        public string Send()
        {
            return "You send one sms!";
        }

        [HttpGet(nameof(Send))]
        public string Send(string message)
        {
            Console.WriteLine(message);
            return "发送了：" + message;
        }
    }
}