using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.Redis
{
    public class RedisOptions
    {
        public bool Startup { get; set; } = false;

        public string HostName { get; set; }

        public string Password { get; set; }

        public int StorageIndex { get; set; }

        /// <summary>
        /// 连接池大小
        /// </summary>
        public int PoolSize { get; set; } = 10;
        /// <summary>
        /// 出队等待时间，毫秒数
        /// </summary>
        public int QueueWait { get; set; } = 600;
    }
}
