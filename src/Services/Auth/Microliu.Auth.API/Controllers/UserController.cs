using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microliu.Auth.Application;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*
 * 模块描述：员工
 * 模块功能：
 *           GetUser        获取用户信息
 *           CreateUser     新增员工
 *           
 */

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
        private  IMapper _mapper { get; }
        public UserController(IAuthApplication authApplication,IMapper mapper)
        {
            _authApplication = authApplication;
            _mapper = mapper;
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
        [Produces("application/json")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserModel createUserModel)
        {
            
            var createdUserId = await _authApplication.CreateUser(createUserModel);
            return Ok(createdUserId);
        }
    }
}