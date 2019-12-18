using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.EventBus
{
    public class EventData : IntegrationEvent
    {
        public object Content { get; set; }
    }
}
