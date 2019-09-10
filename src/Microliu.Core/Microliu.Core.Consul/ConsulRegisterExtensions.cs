using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microliu.Core.Consul
{
    public static class ConsulDiscoveryExtensions
    {
        /// <summary>
        /// 基于客户端的服务发现
        /// </summary>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseMicroliuDiscovery(this IApplicationBuilder app)
        {
            var s = app.ApplicationServices;

            var lifetime = s.GetService<IApplicationLifetime>();
            var configuration = s.GetService<IConfiguration>();

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
                HTTP = $"http://{consulOptions.LocalhostIp}:{consulOptions.LocalhostPort}{consulOptions.HealthCheckPath ?? ""}",// 健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            // 注册服务到Consul
            var registration = new AgentServiceRegistration
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),//不能重复
                Name = consulOptions.ServiceName,
                Address = consulOptions.LocalhostIp,
                Port = consulOptions.LocalhostPort,
                Tags = consulOptions.Tags
            };

            try
            {
                client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
                lifetime.ApplicationStarted.Register(() =>
                {
                });

                lifetime.ApplicationStopping.Register(() =>
                {
                    client.Agent.ServiceDeregister(registration.ID).GetAwaiter().GetResult();// 服务停止后，自动取消注册
                });
            }
            catch (Exception)
            {
                ;
            }
            return app;
        }
    }
}
