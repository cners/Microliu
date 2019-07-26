using System;
using Jimu.Server;

namespace Jimu.Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = new ServiceHostServerBuilder(new Autofac.ContainerBuilder())
         .UseLog4netLogger()
         .LoadServices("QuickStart.Services")
         .UseDotNettyForTransfer("127.0.0.1", 8001)
         .UseConsulForDiscovery("127.0.0.1", 8500, "JimuService")
         ;
            using (var host = hostBuilder.Build())
            {
                host.Run();
                Console.ReadLine();
            }
        }
    }
}
