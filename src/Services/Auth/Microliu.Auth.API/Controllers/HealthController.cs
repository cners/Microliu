using Microsoft.AspNetCore.Mvc;

namespace Microliu.Auth.API.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [ApiVersion("1.0")]
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
    }
}