using DotNetCore.CAP;
using Microliu.EmailService.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.API.Controllers
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ICapPublisher _publisher;

        public EmailController(ICapPublisher capPublisher)
        {
            _publisher = capPublisher;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(nameof(Send))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Send([FromBody]EmailSendDto input)
        {
            _publisher.Publish<EmailSendDto>("microliu.email.send", input);
            return Ok();
        }
    }
}