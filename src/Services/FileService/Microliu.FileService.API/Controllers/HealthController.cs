using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.FileService.API.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {

        /// <summary>
        /// 健康检查方法
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(Check))]
        public IActionResult Check()
        {
            return Ok();
        }

        /// <summary>
        /// 访问测试
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(CheckTest))]
        public IActionResult CheckTest()
        {
            return Ok("欢迎使用分布式文件服务！");
        }
    }
}