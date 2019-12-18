using Microliu.Core.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Core.EventBusTest.EventBus
{
    public class EmailNoticeEvent : IntegrationEvent
    {
      public Guid NoticeId { get; set; }
    }

}
