using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.RedisCache
{
    public class RedisOptions
    {
        /// <summary>
        /// 是否启用redis
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Redis主机地址及端口
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 连接密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 存储数据索引
        /// </summary>
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
