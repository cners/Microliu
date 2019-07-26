using Microliu.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Microliu
{
    public static class MicroliuHelper
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }



        /// <summary>
        /// get config from specify file which locate in app root
        /// </summary>
        /// <param name="fileName">setting json file</param>
        /// <returns></returns>
        public static IConfigurationRoot GetConfig(string settingJson)
        {
            var provider = new JsonEnvParamParserFileProvider(settingJson);
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(provider, settingJson, true, false);
            return builder.Build();
        }

    }
}
