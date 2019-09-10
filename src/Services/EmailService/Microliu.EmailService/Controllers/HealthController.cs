using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("emailApi/[controller]")]
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