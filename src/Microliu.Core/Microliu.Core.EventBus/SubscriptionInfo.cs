using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscrptionsManager
    {

        public class SubscriptionInfo
        {
            public Type HandlerType { get; }

            public SubscriptionInfo(Type handlerType)
            {
                HandlerType = handlerType;
            }

            public static SubscriptionInfo Typed(Type handlerType)
            {
                return new SubscriptionInfo(handlerType);
            }

        }
    }
}
