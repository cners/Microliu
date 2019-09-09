using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microliu.Core.Redis
{
    public static class RedisCacheExtensions
    {
        public static IServiceCollection AddMicroliuRedis(this IServiceCollection services, Action<RedisOptions> setupAction = null)
        {
            //OptionsServiceCollectionExtensions.Configure<RedisOptions>(services, setupAction);

            return services;
        }
    }
}
