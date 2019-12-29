using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Microliu.Core.ELKLoggerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogTestController : ControllerBase
    {
        private readonly ILogger<LogTestController> _logger;

        public LogTestController(ILogger<LogTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {

            _logger.LogInformation($"[ELK日志测试]");
            var rng = new Random();
            return Content("ok");
        }

        [HttpGet(nameof(Add))]
        public IActionResult Add(string content)
        {
            _logger.LogInformation($"{content}");
            return Content(content);
        }

        /// <summary>
        /// 添加x个日志
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        [HttpGet(nameof(AddLogs))]
        public IActionResult AddLogs(int total, string tag)
        {
            Random r = new Random();
            for (int i = 0; i < total; i++)
            {
                _logger.LogInformation(tag + " " + r.Next(10000, 10000000).ToString());
            }
            return Content("ok");
        }
    }
}
