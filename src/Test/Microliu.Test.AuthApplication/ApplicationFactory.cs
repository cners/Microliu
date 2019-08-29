using Microliu.Auth.Application;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
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
            services.AddAutoMapper();
            services.AddAuthService(configurationBuilder.Build()); // Application层扩展了所需注入的信息，看吧这里就体现该功能的作用了，多个api或测试库使用避免了多次配置注入
            _services = services.BuildServiceProvider();
            IApplicationBuilder builder = new ApplicationBuilder(_services);
            var expression =builder.UseAutoMapper();
            expression.CreateMap<User, CreateUserModel>();
            builder.UseStateAutoMapper();

        }

        public static IAuthService GetIAuthApplication()
        {
            InitIoc();
            return _services.GetService<IAuthService>();
        }
    }
}
