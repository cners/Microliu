using Microliu.Core.EventBus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Core.EventBusTest.EventBus
{
    public class EmailNoticeEventHandler : IIntegrationEventHandler<EmailNoticeEvent>
    {
        private readonly ILogger<EmailNoticeEventHandler> _logger;
        public EmailNoticeEventHandler(ILogger<EmailNoticeEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(EmailNoticeEvent @event)
        {
            _logger.LogDebug($"正在执行 {nameof(EmailNoticeEventHandler)}.Handle");
        }
    }
}
