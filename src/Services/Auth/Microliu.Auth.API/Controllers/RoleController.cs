using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

/*
 * 模块描述：角色
 * 模块功能：
 *           CreateRole     创建角色
 *           Delete         删除角色
 *           UpdateRoleName 修改角色名称
 *           
 */

namespace Microliu.Auth.API.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [ApiController]
    [Produces("application/json")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public RoleController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        // POST: api/auth/createrole
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <remarks>
        /// POST /api/v1.0/auth/createRole
        /// {
        ///     "RoleName":"",
        ///     "CreatorId":"",
        ///     "Sort":10
        /// }
        /// </remarks>
        /// <returns></returns>
        //[MapToApiVersion("1.0")]
        [HttpPost(nameof(CreateRole))]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleModel createRole)
        {
            await _authApplication.CreateRole(createRole);
            return Ok("CreateRole");
        }


        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns>已删除的角色主键</returns>
        [HttpGet(nameof(Delete))]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _authApplication.RemoveRole(id);
            return Ok(id);
        }


        /// <summary>
        /// 修改角色名称
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <param name="newRoleName">新角色名称</param>
        /// <returns></returns>
        [HttpGet(nameof(UpdateRoleName))]
        public async Task<IActionResult> UpdateRoleName(string id, string newRoleName)
        {
            var result = await _authApplication.UpdateRoleName(id, newRoleName);
            return Ok(result);
        }
    }
}