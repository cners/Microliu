using Exceptionless;
using Hangfire;
using Hangfire.SqlServer;
using Microliu.Core.Logger;
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

            //services.AddSingleton<EmailServiceSettings>(GetEmailService(configuration));
            services.Configure<EmailServiceSettings>(_ => configuration.GetSection("EmailService").Bind(_));

            // Add Hangfire services.
            services.AddHangfire(c => c
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(GetConnectionString(configuration, DatabaseType.SQLServer), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

           
            return services;
        }

        public static IApplicationBuilder UseEmailService(this IApplicationBuilder builder)
        {
            var services = builder.ApplicationServices;

            var configuration = services.GetService<Microsoft.Extensions.Configuration.IConfiguration>();
            var backgroundJobs = services.GetService<IBackgroundJobClient>();
            builder.UseHangfireDashboard();
            builder.UseHangfireDashboard();
            //backgroundJobs.Enqueue(() => Console.WriteLine("UseEmailService"));

            //ExceptionlessClient.Default.Configuration.ApiKey = configuration.GetSection("Exceptionless:ApiKey").Value;
            //ExceptionlessClient.Default.Configuration.ServerUrl = configuration.GetSection("Exceptionless:ServerUrl").Value;

            builder.UseExceptionless(configuration);

            return builder;
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

        private static EmailServiceSettings GetEmailService(IConfiguration configuration)
        {
            return new EmailServiceSettings
            {
                Sender = new EmailServiceSettings.EmailSender
                {
                    Name = configuration.GetSection("EmailService:Sender:Name").Value,
                    Password = configuration.GetSection("EmailService:Sender:Password").Value
                }
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
    }
}
