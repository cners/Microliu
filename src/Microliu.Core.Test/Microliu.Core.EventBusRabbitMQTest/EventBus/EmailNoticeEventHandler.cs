using Microliu.Core.EventBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            _logger.LogInformation("执行事件 {EventHandlerName}.Handle,eventId:{EventId},creationTime:{CreationTime},\r\n{Data}",
                nameof(EmailNoticeEventHandler), @event.Id, @event.CreationTime, JsonConvert.SerializeObject(@event));
        }
    }
}
