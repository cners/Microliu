using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.EventBusRabbitMQ
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddEasyNetQ(this IServiceCollection services, IConfiguration configuration)
        {
            // IoC - EventBus
            services.AddSingleton(RabbitHutch.CreateBus(configuration["MQ:Dev"]));

            return services;
        }
    }
}
