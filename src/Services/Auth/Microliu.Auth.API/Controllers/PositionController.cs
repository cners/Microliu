using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/*
 * 模块描述：岗位
 * 模块功能：
 *           CreatePosition     创建岗位
 *           SetUserPosition    给员工分配岗位
 *           GetUsers           当前岗位的员工集合
 *           GetPosition        获取岗位信息
 *           
 */

namespace Microliu.Auth.API.Controllers
{
    /// <summary>
    /// 岗位
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IAuthService _authApplication;

        public PositionController(IAuthService authApplication)
        {
            _authApplication = authApplication;
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(CreatePosition))]
        public async Task<IActionResult> CreatePosition([FromBody]CreatePositionModel input)
        {
            await _authApplication.CreatePosition(input);
            return Ok();
        }

        /// <summary>
        /// 给员工分配岗位
        /// </summary>
        /// <param name="userId">员工主键</param>
        /// <param name="positionId">岗位主键</param>
        /// <returns></returns>
        [HttpGet(nameof(SetUserPosition))]
        public async Task<IActionResult> SetUserPosition(string userId, string positionId)
        {
            await _authApplication.SetUserPosition(userId, positionId);
            return Ok();
        }


        /// <summary>
        /// 当前岗位的员工集合
        /// </summary>
        /// <param name="positionId">岗位主键</param>
        /// <returns></returns>
        [HttpGet(nameof(GetUsers))]
        public IActionResult GetUsers(string positionId)
        {
            var users = _authApplication.GetUsers(positionId);
            return Ok(users);
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="id">岗位主键</param>
        /// <returns></returns>
        [HttpGet(nameof(GetPosition))]
        public IActionResult GetPosition(string id)
        {
            dynamic positions = _authApplication.GetPosition(id);
            return Ok(positions);
        }
    }
}