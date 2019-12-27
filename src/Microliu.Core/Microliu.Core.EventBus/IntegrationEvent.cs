using Newtonsoft.Json;
using System;

namespace Microliu.Core.EventBus
{
    /// <summary>
    /// 集成事件
    /// </summary>
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationTime = createDate;
        }

        /// <summary>
        /// 事件ID
        /// </summary>
        [JsonProperty]
        public Guid Id { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTime CreationTime { get; private set; }
    }
}
