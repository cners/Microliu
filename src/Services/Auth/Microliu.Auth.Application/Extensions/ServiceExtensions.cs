using AutoMapper;
using Microliu.Auth.DataMySQL;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Repositories;
using Microliu.Auth.Domain.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microliu.Auth.Application
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddAuthService(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddTransient<IAuthService, AuthApplication>();// 权限服务

            services.AddTransient<IRoleRepository, RoleRepository>();// 角色
            services.AddTransient<IUserRepository, UserRepository>();// 员工
            services.AddTransient<IPositionRepository, PositionRepository>();// 岗位

            services.AddDbContextPool<AuthDbContext>(options =>
            {
                options.UseMySQL(GetConnectionString(configuration, DatabaseType.MySQL));// 可切换数据库源UseOracle,UseSqlserver
            }, poolSize: 64);

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddCap(x =>
            {
                // 使用CAP控制面板
                x.UseDashboard();


                // 如果你的 SqlServer 使用的 EF 进行数据操作，你需要添加如下配置：
                // 注意: 你不需要再次配置 x.UseSqlServer(""")
                //x.UseEntityFramework<AuthDbContext>();

                x.UseMySql(GetConnectionString(configuration, DatabaseType.MySQL));

                var eventBus = GetvEventBusCAP(configuration);
                // 如果你使用的 RabbitMQ 作为MQ，你需要添加如下配置：
                x.UseRabbitMQ(options =>
                {
                    options.HostName = eventBus.HostName ?? "192.168.10.214";
                    options.UserName = eventBus.UserName ?? "microliu";
                    options.Password = eventBus.Password ?? "microliu";
                    options.Port = eventBus.Port;
                    options.VirtualHost = eventBus.VirtualHost ?? "MICROLIU";
                    options.ExchangeName = eventBus.ExchangeName ?? "Microliu";
                });
                //设置处理成功的数据在数据库中保存的时间（秒），为保证系统性能，数据会定期清理。
                //x.SucceedMessageExpiredAfter = 24 * 3600;

                //设置失败重试次数
                x.FailedRetryCount = 5;

                // 消费者线程数量
                x.ConsumerThreadCount = 2;

            });

            return services;
        }

        private enum DatabaseType
        {
            MySQL = 1,
            SQLServer = 2,
            Oracle = 4
        }
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static string GetConnectionString(IConfiguration configuration, DatabaseType databaseType)
        {
            string connection = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (databaseType == DatabaseType.MySQL)
                {
                    connection = configuration.GetConnectionString("authServiceWindowsMySQL") ?? "";
                }
                else if (databaseType == DatabaseType.SQLServer)
                {
                    connection = configuration.GetConnectionString("authServiceWindowsMSSQL") ?? "";
                }
                else if (databaseType == DatabaseType.Oracle)
                {
                    connection = configuration.GetConnectionString("authServiceWindowsOracle") ?? "";
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("authServiceDocker") ?? "";
            }

            return connection;
        }

        private static EventBusCAP GetvEventBusCAP(IConfiguration configuration)
        {
            return new EventBusCAP
            {
                HostName = configuration.GetSection("EventBusCAP:RabbitMQ:HostName").Value,
                UserName = configuration.GetSection("EventBusCAP:RabbitMQ:UserName").Value,
                Password = configuration.GetSection("EventBusCAP:RabbitMQ:Password").Value,
                Port = int.Parse(configuration.GetSection("EventBusCAP:RabbitMQ:Port").Value),
                VirtualHost = configuration.GetSection("EventBusCAP:RabbitMQ:VirtualHost").Value,
                ExchangeName = configuration.GetSection("EventBusCAP:RabbitMQ:ExchangeName").Value,
            };
        }


        class EventBusCAP
        {
            public string HostName { get; set; }
            public string UserName { get; set; }

            public string Password { get; set; }

            public int Port { get; set; }

            public string VirtualHost { get; set; }

            public string ExchangeName { get; set; }
        }
        /// <summary>
        /// 扩展满足获取不同数据库源的服务接口
        /// 需要实现GetDbType方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        //public static T GetServices<T>(this IServiceProvider provider, DbType dbType)
        //{
        //    var services = provider.GetServices<T>();
        //    return services.Where(r => (DbType)r.GetType().GetMethod("GetDbType").Invoke(r, null) == dbType).FirstOrDefault();
        //}
    }

    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection service)
        {
            service.TryAddSingleton<AutoMapper.Configuration.MapperConfigurationExpression>();
            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfigurationExpression = serviceProvider.GetRequiredService<AutoMapper.Configuration.MapperConfigurationExpression>();
                var instance = new MapperConfiguration(mapperConfigurationExpression);

                instance.AssertConfigurationIsValid();

                return instance;
            });
            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfiguration = serviceProvider.GetRequiredService<MapperConfiguration>();

                return mapperConfiguration.CreateMapper();
            });

            return service;
        }

        public static IMapperConfigurationExpression UseAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.ApplicationServices.GetRequiredService<AutoMapper.Configuration.MapperConfigurationExpression>();
        }
    }

    public static class AutoMapperHelper
    {
        private static IServiceProvider ServiceProvider;

        public static void UseStateAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
        }

        public static TDestination Map<TDestination>(object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();

            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
