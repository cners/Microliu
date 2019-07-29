﻿using Autofac;
using Microsoft.Extensions.Logging;
using Surging.Core.Caching.Configurations;
using Surging.Core.Consul.Configurations;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Configurations;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Surging.Core.ProxyGenerator;
using Surging.Core.ServiceHosting;
using Surging.Core.ServiceHosting.Internal.Implementation;
using System;

namespace NewsHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHostBuilder()
                .RegisterServices(builder =>
                {
                    builder.AddMicroService(option =>
                    {
                        option.AddServiceRuntime()
                              .AddClientProxy()
                              .AddRelateServiceRuntime()
                              .AddConfigurationWatch()
                              .AddServiceEngine(typeof(SurgingServiceEngine));

                        builder.Register(p => new CPlatformContainer(ServiceLocator.Current));
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConfiguration(AppConfig.GetSection("Logging"));
                })
                .UseServer(options => { })
                .UseConsoleLifetime()
                .Configure(build =>
                {
#if DEBUG
                    build.AddCacheFile("${cachePath}|/app/configs/cacheSettings.json", optional: false, reloadOnChange: true);
                    build.AddCPlatformFile("${surgingPath}|/app/configs/surgingSettings.json", optional: false, reloadOnChange: true);
                    build.AddEventBusFile("/app/configs/eventBusSettings.json", optional: false);//${eventBusPath}|
                    build.AddConsulFile("${consulPath}|/app/configs/consul.json", optional: false, reloadOnChange: true);


#else
                    build.AddCacheFile("${cachePath}|configs/cacheSettings.json", optional: false, reloadOnChange: true);                      
                    build.AddCPlatformFile("${surgingPath}|configs/surgingSettings.json", optional: false,reloadOnChange: true);                    
                    build.AddEventBusFile("configs/eventBusSettings.json", optional: false);
                    build.AddConsulFile("configs/consul.json", optional: false, reloadOnChange: true);
#endif
                })
                .UseProxy()
                .UseStartup<Startup>()
                .Build();

            using (host.Run())
            {
                Console.WriteLine($"服务主机 [NewsHost] 启动成功 {DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
#if DEBUG
                //if (AppConfig.ServerOptions.)
                {
                    Startup.InitActions();
                }
#endif
            }
        }
    }
}
