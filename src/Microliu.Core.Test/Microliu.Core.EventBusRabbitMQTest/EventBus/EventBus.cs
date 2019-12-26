using Exceptionless.Dependency;
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
        private readonly IEventBusSubscriptionsManager _eventManager;
        private readonly IServiceProvider _serviceProvider;

        public EventBus(ILogger<EventBus> logger,
            IEventBusSubscriptionsManager eventBusSubscrptionsManager,
            IServiceProvider serviceProvider = null)
        {
            _logger = logger;
            _eventManager = eventBusSubscrptionsManager;
            _serviceProvider = serviceProvider;
        }

        public bool Publish(IntegrationEvent @event)
        {
            _logger.LogInformation("EventBus Start Publish");
            if (!_eventManager.HasSubscriptionsForEvent<TEvent>())
            {
                return false;
            }

            var handlers = _eventManager.GetHandlersForEvent<TEvent>();
            var handlerTasks = new List<Task>();

            foreach (var handlerType in handlers)
            {
                if (_serviceProvider.GetServiceOrCreateInstance(handlerType) is IIntegrationEventHandler<TEvent> handler)
                {
                    handlerTasks.Add(handler.Handle(@event));

                }

            }
            //handlerTasks

            return true;
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>
        {
            _logger.LogInformation("EventBus Start Subscribe");
            _eventManager.AddSubscription<TEvent, TEventHandler>();
        }

        public void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>
        {
            _logger.LogInformation("EventBus Start Unsubscribe");
            _eventManager.RemoveSubscription<TEvent, TEventHandler>();
        }
    }
}
