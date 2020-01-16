using Microliu.Core.RedisCache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.RedisTest.Container
{
    public class RedisFactory
    {
        private static IServiceProvider _services;

        private static void InitIoC()
        {
            if (_services != null) return;

            IServiceCollection services = new ServiceCollection();


            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(config);

            ILoggerFactory loggerFactory = new LoggerFactory();
            services.AddSingleton<ILogger<CacheService>>(loggerFactory.CreateLogger<CacheService>());
             
            services.AddRedis();
            _services = services.BuildServiceProvider();

            IApplicationBuilder builder = new ApplicationBuilder(_services);
            builder.Build();
        }

        public static ICacheService Get()
        {
            InitIoC();
            return _services.GetService<ICacheService>();
        }

        public static ILogger GetLogger()
        {
            return _services.GetService<ILogger<CacheService>>();
        }
    }
}
