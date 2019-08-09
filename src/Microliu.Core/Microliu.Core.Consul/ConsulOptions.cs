using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.Consul
{
    public class ConsulOptions
    {
        /// <summary>
        /// consul ip
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// consul port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// which groups to extract, multiple seperate with ','
        /// </summary>
        public string ServiceGroups { get; set; }

        public string ServiceName { get; set; }


        public string[] Tags { get; set; }

        public string LocalhostIp { get; set; }
        public int LocalhostPort { get; set; }

        public string HealthCheckPath { get; set; }

        public ConsulOptions(string ip, int port, string serviceGroups)
        {
            this.IP = ip;
            this.Port = port;
            this.ServiceGroups = serviceGroups;
        }

        public ConsulOptions() { }
    }
}
