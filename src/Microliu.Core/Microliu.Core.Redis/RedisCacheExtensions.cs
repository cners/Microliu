using Microliu.Core.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microliu.Core.RedisCache
{
    public static class RedisCacheExtensions
    {
        public static IServiceCollection AddMicroliuRedis(this IServiceCollection services, Action<RedisOptions> setupAction = null)
        {
            var logger = services.BuildServiceProvider().GetService<ILogger>();
            RedisOptions options = new RedisOptions();
            setupAction.Invoke(options);
            CacheService cacheService = new CacheService(options, logger);
            services.AddSingleton<ICacheService>(cacheService);
            return services;
        }
    }
}
