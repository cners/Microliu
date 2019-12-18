using Microliu.Core.EventBus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Core.EventBusTest.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly ILogger<EventBus> _logger;

        public EventBus(ILogger<EventBus> logger)
        {
            _logger = logger;
        }

        public void Publish(IntegrationEvent @event)
        {
            _logger.LogDebug("publish");
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler
        {
            _logger.LogDebug("subscribe");
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler
        {
            _logger.LogDebug("unsubscribe");
        }
    }
}
