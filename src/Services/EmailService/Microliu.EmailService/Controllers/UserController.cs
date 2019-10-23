using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microliu.EmailService.Application;
using Microliu.EmailService.Application.ViewModel;
using Microliu.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private ReturnResult _return;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _return = new ReturnResult();
            _return.SetSuccess(false);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        [HttpGet("Register/{email}/{password}")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var r = await _userService.Create(email, password);
            return Ok(r);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="uid">用户主键</param>
        /// <returns></returns>
        [HttpGet("{uid}")]
        public IActionResult GetEntity(long uid)
        {
            var userinfo = _userService.GetUserInfo(uid);
            if (userinfo != null)
            {
                return Ok(_return.SetData(true, userinfo));
            }
            return Ok(_return);
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody]UserLoginDto dto)
        {
            return Ok(_userService.Login(dto));
        }


        /// <summary>
        /// 更换邮箱
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(nameof(ChangeEmail))]
        public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmailDto dto)
        {
            if (dto == null) return Ok(_return.SetMessage("参数有误"));
            var r = await _userService.ChangeEmail(dto.Email, dto.Password, dto.NewEmail);
            return Ok(r);
        }
    }
}