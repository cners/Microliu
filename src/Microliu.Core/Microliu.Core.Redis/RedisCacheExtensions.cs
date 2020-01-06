using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Microliu.Core.RedisCache
{
    public static class RedisCacheExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, Action<RedisOptions> setupAction = null)
        {
            var logger = services.BuildServiceProvider().GetService<ILogger<CacheService>>();
            RedisOptions options = new RedisOptions();
            setupAction.Invoke(options);
            CacheService cacheService = new CacheService(options, logger);
            services.AddSingleton<ICacheService>(cacheService);
            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<CacheService>>();
            var config = serviceProvider.GetService<IConfiguration>();

            var options = config.GetSection("Redis").Get<RedisOptions>();
            if (options == null || options.Enabled == false)
            {
                return services;
            }
            else
            {
                CacheService cacheService = new CacheService(options, logger);
                services.AddSingleton<ICacheService>(cacheService);
                return services;
            }

        }

    }
}
