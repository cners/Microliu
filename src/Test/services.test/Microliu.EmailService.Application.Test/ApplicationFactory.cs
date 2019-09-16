using Microliu.EmailService.Application.Extensions;
using Microliu.EmailService.Application.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microliu.EmailService.Application.Test
{
    public  class ApplicationFactory
    {

        private static IServiceProvider _services;
        private static void InitIoc()
        {

            if (_services != null) return;

            IServiceCollection services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<IApplicationLifetime, ApplicationLifetime>();

            services.AddEmailService();
            _services = services.BuildServiceProvider();

            IApplicationBuilder builder = new ApplicationBuilder(_services);
            builder.UseEmailService();
            
        }

        public static IEmailService GetIEmailApplication()
        {
            InitIoc();
            return _services.GetService<IEmailService>();
        }
    }
}
