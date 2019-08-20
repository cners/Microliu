using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microliu.Auth.Application;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.Auth.API.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;
        public UserController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户主键</param>
        /// <returns></returns>
        [HttpGet(nameof(GetUser))]
        public async Task<IActionResult> GetUser(string id)
        {
            var users = _authApplication.GetUser(id);
            return Ok(users);
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="createUserModel"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserModel createUserModel)
        {
            var createdUserId = await _authApplication.CreateUser(createUserModel);
            return Ok(createdUserId);
        }
    }
}