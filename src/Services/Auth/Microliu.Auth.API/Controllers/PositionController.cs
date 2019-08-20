using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microliu.Auth.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.Auth.API.Controllers
{
    /// <summary>
    /// 岗位
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public PositionController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(CreatePosition))]
        public async Task<IActionResult> CreatePosition()
        {
            await _authApplication.CreatePosition();
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
            var positions = _authApplication.GetPosition(id);
            return Ok(positions);
        }
    }
}