using Autofac;
using Microliu.Core.EventBus;
using Microliu.Core.EventBus.RabbitMQ;
using Microliu.Core.EventBusRabbitMQOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microliu.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection services, string clientName)
        {
            var options = new RabbitMQOptions();
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                config.GetSection("RabbitMQ").Bind(options);

                var factory = new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    DispatchConsumersAsync = true,
                };
                factory.UserName = options.UserName;
                factory.Password = options.Password;
                factory.VirtualHost = options.VirtualHost;

                var retryCount = options.RetryCount;

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


            var subscriptionClientName = clientName ?? "EventBusDefaultName";

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var loggerInMemory = sp.GetRequiredService<ILogger<InMemoryEventBusSubscriptionsManager>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection,
                                            logger,
                                            iLifetimeScope,
                                            eventBusSubcriptionsManager,
                                            loggerInMemory,
                                            options.ExchangeName,
                                            subscriptionClientName,
                                            options.RetryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            return services;
        }

        public static IServiceCollection RegisterEventBusHandler<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddTransient(typeof(TService));
            return services;
        }


        public static IServiceCollection RegisterEventBusHandler(this IServiceCollection services)
        {
            //集中注册服务
            foreach (var item in GetClassName("Handler"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }
            return services;
        }

        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        public static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}
