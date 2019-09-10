using Exceptionless;
using Hangfire;
using Hangfire.Redis;
using Hangfire.SqlServer;
using Microliu.Core.Loggers;
using Microliu.Core.Redis;
using Microliu.Core.RedisCache;
using Microliu.EmailService.Application.IServices;
using Microliu.EmailService.Application.Services;
using Microliu.EmailService.Data;
using Microliu.EmailService.Data.Repositories;
using Microliu.EmailService.Domain.Repositories;
using Microliu.EmailService.Domain.SeedWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.InteropServices;

namespace Microliu.EmailService.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<Microsoft.Extensions.Configuration.IConfiguration>();
            services.AddTransient<ILogger, Logger>();
            services.AddTransient<IEmailService, EmailApplication>(); //邮件服务

            services.AddTransient<IEmailSendRepository, EmailSendRepository>();// 邮件发送

            services.AddDbContextPool<EmailDbContext>(options =>
            {
                options.UseMySQL(GetConnectionString(configuration, DatabaseType.MySQL));
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

                var eventBus = new EventBusCAP();
                configuration.GetSection("EventBusCAP:RabbitMQ").Bind(eventBus);
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

            services.Configure<EmailServiceOptions>(_ => configuration.GetSection("EmailService").Bind(_));

            // Add Hangfire services.
            services.AddHangfire(c =>
            {
                var prefix = configuration.GetSection("Hangfire:Prefix").Value;
                var host = configuration.GetSection("Hangfire:Redis").Value;
                var defualtDb = configuration.GetSection("Hangfire").GetValue<int>("DefaultDb");
                var options = new RedisStorageOptions { Prefix = $"{prefix}:", ExpiryCheckInterval = TimeSpan.FromSeconds(60 * 5), Db = defualtDb, DeletedListSize = 2000, SucceededListSize = 2000 };
                c
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRedisStorage(host, options)
                .UseRecommendedSerializerSettings();
            });

            #region hangfire的MSSQL持久化支持，暂时不需要，性能不太好，而且延迟任务不准时
            //.UseSqlServerStorage(GetConnectionString(configuration, DatabaseType.SQLServer)));
            //, new SqlServerStorageOptions
            //  {
            //      CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //      SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //      QueuePollInterval = TimeSpan.Zero,
            //      UseRecommendedIsolationLevel = true,
            //      UsePageLocksOnDequeue = true,
            //      DisableGlobalLocks = true
            //  }
            #endregion

            // Redis Cache
            var redisOptions = new RedisOptions();
            configuration.GetSection("Redis").Bind(redisOptions);
            services.AddMicroliuRedis(options => options = redisOptions);

            return services;
        }

        public static IApplicationBuilder UseEmailService(this IApplicationBuilder app)
        {
            //var services = app.ApplicationServices;

            //var configuration = services.GetService<Microsoft.Extensions.Configuration.IConfiguration>();
            //var backgroundJobs = services.GetService<IBackgroundJobClient>();

            // 设置Hangfire并发处理Job的个数，默认是 cpu个数*5
            var options = new BackgroundJobServerOptions { WorkerCount = 10 };
            // 启用HangfireServer这个中间件（它会自动释放）
            app.UseHangfireServer(options);
            // 启用Hangfire的仪表盘（可以看到任何的状态，进度等信息）
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {

            });
            //backgroundJobs.Enqueue(() => Console.WriteLine("UseEmailService"));

            //ExceptionlessClient.Default.Configuration.ApiKey = configuration.GetSection("Exceptionless:ApiKey").Value;
            //ExceptionlessClient.Default.Configuration.ServerUrl = configuration.GetSection("Exceptionless:ServerUrl").Value;

            //builder.UseExceptionless(configuration);

            return app;
        }

        private enum DatabaseType
        {
            MySQL = 1,
            SQLServer = 2,
            Oracle = 4
        }

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

     

        class EventBusCAP
        {
            public string HostName { get; set; }
            public string UserName { get; set; }

            public string Password { get; set; }

            public int Port { get; set; }

            public string VirtualHost { get; set; }

            public string ExchangeName { get; set; }
        }
    }
}
