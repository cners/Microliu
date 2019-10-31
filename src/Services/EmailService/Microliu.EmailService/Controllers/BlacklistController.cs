using System.Threading.Tasks;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Application.ViewModel;
using Microliu.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BlacklistController : ControllerBase
    {
        private readonly IBlacklistService _blacklistService;
        private ReturnResult _return;

        public BlacklistController(IBlacklistService blacklistService)
        {
            _blacklistService = blacklistService;
            _return = new ReturnResult();
            _return.SetSuccess(false);
        }


        /// <summary>
        /// 加入黑名单
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        [HttpGet("addd/{email}")]
        public async Task<IActionResult> Add(string email)
        {
            var r = await _blacklistService.CreateBlacklist(email);
            return Ok(r);
        }


        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(nameof(List))]
        public IActionResult List([FromBody]BlacklistQueryDto dto)
        {
            var r = _blacklistService.GetList(dto);
            return Ok(_return.SetData(true, r));
        }

        /// <summary>
        /// 移除黑名单
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        [HttpGet("remove/{email}")]
        public async Task<IActionResult> Remove(string email)
        {
            var r = await _blacklistService.RemoveBlackEmail(email);
            return Ok(r);
        }
    }
}