using System;
using System.Collections.Generic;
using System.Text;
using static Microliu.Core.EventBus.InMemoryEventBusSubscriptionsManager;

namespace Microliu.Core.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 事件移除
        /// </summary>
        event EventHandler<string> OnEventRemoved;

        /// <summary>
        /// 添加订阅者
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        void AddSubscription<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;

        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        void RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;


        bool HasSubscriptionsForEvent<TEvent>() where TEvent : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);
        
        
        /// <summary>
        /// 清空订阅
        /// </summary>
        void Clear();


        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<TEvent>();
    }
}
