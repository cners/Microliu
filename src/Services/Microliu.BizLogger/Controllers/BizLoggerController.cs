using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.BizLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BizLoggerController : ControllerBase
    {
        // api/bizlogger/list
        [Route("list")]
        [HttpGet]
        public IActionResult List()
        {
            var list = new List<string>
            {
                "Log1",
                "log2"
            };
            return Ok(list);
        }

        [HttpGet]
        [Route(nameof(GetLogs))]
        public IActionResult GetLogs()
        {
            return Ok("你好，恭喜你迈出了RPC的第一步");
        }
    }
}