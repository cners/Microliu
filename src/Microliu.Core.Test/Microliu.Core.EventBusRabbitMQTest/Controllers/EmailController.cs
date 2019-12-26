using System;
using Microliu.Core.EventBus;
using Microliu.Core.EventBusTest.EventBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microliu.Core.EventBusRabbitMQTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;

        public EmailController(ILogger<EmailController> logger,
            IEventBus eventBus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet(nameof(Send))]
        public IActionResult Send()
        {
            _logger.LogDebug("Debug");
            _logger.LogTrace("Trace");
            var emailNotice = EmailNoticeEvent.Default();
            _eventBus.Publish(emailNotice);
            return new JsonResult(emailNotice);
        }
    }
}