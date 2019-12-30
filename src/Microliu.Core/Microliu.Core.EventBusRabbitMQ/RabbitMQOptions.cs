using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.EventBusRabbitMQOptions
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string VirtualHost { get; set; }
        public int RetryCount { get; set; }

        public string ExchangeName { get; set; }

        public RabbitMQOptions()
        {
            RetryCount = 5;
        }
    }
}
