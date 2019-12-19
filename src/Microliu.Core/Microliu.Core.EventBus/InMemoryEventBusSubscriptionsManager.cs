using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.Core.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly ILogger<InMemoryEventBusSubscriptionsManager> _logger;

        public event EventHandler<string> OnEventRemoved;


        public InMemoryEventBusSubscriptionsManager(ILogger<InMemoryEventBusSubscriptionsManager> logger)
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
            _logger = logger;
        }

        public bool IsEmpty => _handlers.Keys.Any();
        public void Clear() => _handlers.Clear();


        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            if (!HasSubscriptionsForEvent<T>())
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            var @typeof = typeof(T);
            if (_handlers[eventName].Any(s => s.HandlerType == @typeof))
            {
                _logger.LogWarning($"Handler Type {@typeof.Name} aleardy registered for '{eventName}'");

                throw new ArgumentException($"Handler Type {@typeof.Name} aleardy registered for '{eventName}'", nameof(@typeof));
            }
            _handlers[eventName].Add(SubscriptionInfo.Typed(@typeof));

            _logger.LogInformation($"Added one subscription successed.eventName:{eventName}");

            if (!_eventTypes.Contains(@typeof))
            {
                _eventTypes.Add(@typeof);
            }
        }
        public void RemoveSubscription<T, TH>()
         where T : IntegrationEvent
         where TH : IIntegrationEventHandler<T>
        {
            var handlerToRemove = FindHandlerToRemove<T, TH>();
            
            // do remove handler
            if (handlerToRemove != null)
            {
                var eventName = GetEventKey<T>();
                _handlers[eventName].Remove(handlerToRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if(eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var eventName = GetEventKey<T>();
            return GetHandlersForEvent(eventName);
        }
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];


        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var eventName = GetEventKey<T>();
            return HasSubscriptionsForEvent(eventName);
        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);
        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);
     


        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        private SubscriptionInfo FindHandlerToRemove<T,TH>()
            where T:IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            if (!HasSubscriptionsForEvent<T>())
            {
                return null;
            }

            var eventName = GetEventKey<T>();
            var @typeof = typeof(TH);
            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == @typeof);
        }


        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }
    }
}
