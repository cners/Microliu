using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
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
    }
}