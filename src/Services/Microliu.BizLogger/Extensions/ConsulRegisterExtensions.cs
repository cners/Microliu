using Consul;
using Microliu.BizLogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

public static class ConsulDiscoveryExtensions
{


    /*
     如何使用：
     请在appsettings.json中加入

     "ConsulOptions": {
        "Ip": "localhost",
        "Port": 8500,
        "ServiceGroups": "",
        "ServiceName": "BizLogger",
        "Tags": [ "BizLogger", "业务日志服务"],
        "LocalhostIp": "localhost",
        "LocalhostPort": 9950
      }
     */

    /// <summary>
    /// 服务注册到Consul
    /// </summary>
    /// <param name="app">IApplicationBuilder</param>
    /// <param name="lifetime">IApplicationLifetime</param>
    /// <returns>IApplicationBuilder</returns>
    public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app,
                                                         IApplicationLifetime lifetime,
                                                         IConfiguration configuration)
    {
        var consulOptions = configuration.GetSection(typeof(ConsulOptions).Name).Get<ConsulOptions>();
        if (consulOptions == null)
        {
            return app;
        }


        var client = new ConsulClient(x => x.Address = new Uri($"http://{consulOptions.IP}:{consulOptions.Port}"));//请求注册Consul的地址
        var httpCheck = new AgentServiceCheck
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),// 服务启动多久后注册
            Interval = TimeSpan.FromSeconds(1), // 健康检查时间间隔，或心跳间隔
            HTTP = $"http://{consulOptions.LocalhostIp}:{consulOptions.LocalhostPort}/api/health/check",// 健康检查地址
            Timeout = TimeSpan.FromSeconds(5)
        };

        // 注册服务到Consul
        var registration = new AgentServiceRegistration
        {
            Checks = new[] { httpCheck },
            ID = Guid.NewGuid().ToString(),//不能重复
            Name = consulOptions.ServiceName,
            Address = consulOptions.IP,
            Port = consulOptions.Port,
            Tags = consulOptions.Tags 
        };

        client.Agent.ServiceRegister(registration).Wait();// 服务启动注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
        lifetime.ApplicationStopping.Register(() =>
        {
            client.Agent.ServiceDeregister(registration.ID).Wait();// 服务停止后，自动取消注册
        });
        return app;
    }
}

