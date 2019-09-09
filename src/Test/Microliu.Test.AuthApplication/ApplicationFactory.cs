using Microliu.Auth.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microliu.Test
{
    public class ApplicationFactory
    {
        private static IAuthService _authApplication;

        private static IServiceProvider _services;
        private static void InitIoc()
        {
            //var container = new UnityContainer();  //采用这个不符合Application的IOC

            if (_services != null) return;

            IServiceCollection services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            services.AddSingleton<IConfiguration>(configurationBuilder.Build());

            services.AddAuthService();
            _services = services.BuildServiceProvider();

            IApplicationBuilder builder = new ApplicationBuilder(_services);
            builder.UseAuthService();
        }

        public static IAuthService GetIAuthApplication()
        {
            InitIoc();
            return _services.GetService<IAuthService>();
        }
    }
}
