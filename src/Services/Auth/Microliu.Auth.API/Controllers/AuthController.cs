using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Microliu.Auth.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Produces("application/json")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRoleAppService _authAppService;

        public AuthController(IRoleAppService authAppService)
        {
            _authAppService = authAppService;
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
        [HttpGet(nameof(CreateRole))]
        public async Task<IActionResult> CreateRole()
        {
            var role = new CreateRoleModel
            {
                RoleName = "TestRoleName",
                CreatorId = "liu",
                Sort = 10
            };
            await _authAppService.CreateRole(role);
            return Ok("CreateRole");
        }

        //[MapToApiVersion("2.0")]
        [HttpGet(nameof(Query))]
        public IActionResult Query()
        {
            return Ok("query");
        }

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns>已删除的角色主键</returns>
        [HttpGet(nameof(Delete))]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _authAppService.RemoveRole(id);
            return Ok(id);
        }
    }
}