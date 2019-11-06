using DotNetCore.CAP;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.EmailService.API.Controllers
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        //private readonly ICapPublisher _publisher;
        private readonly IEmailService _emailService;

        public EmailController(ICapPublisher capPublisher, IEmailService emailService)
        {
            //_publisher = capPublisher;
            _emailService = emailService;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// 
        /// <remarks>
        /// {<br/>
        ///  "subject": "主题",<br/>
        ///  "body": "内容（支持Html）",<br/>
        ///  "to": "收件人（多个用;分号分割）",<br/>
        ///  "copyTo": "抄送人（多个用;分号分割）",<br/>
        ///  "projectId": "归属项目标识"<br/>
        ///}<br/>
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(nameof(Send))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Send([FromBody]EmailSendDto dto)
        {
            //_publisher.Publish<EmailSendDto>("microliu.email.send", input);

            return Ok(_emailService.SendAsync(dto));
        }
    }
}