using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microliu.Core.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        private readonly ILogger _logger;

        public DefaultController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet(nameof(Welcome))]
        public IActionResult Welcome()
        {
            _logger.Debug("欢迎使用邮件服务");
            return Ok("欢迎使用邮件服务");
        }
    }
}