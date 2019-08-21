using Microsoft.AspNetCore.Mvc;
/*
 * 模块描述：健康检查
 * 模块功能：
 *           Check     健康检查方法
 *           
 */
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