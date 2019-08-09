using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microliu.Core.Hystrix.Extensions.DI
{
    public static class StartupExtensions
    {

        public static IServiceProvider UseHystrix(this IServiceCollection services, Assembly assembly)//把这个方法的返回值由原来的void改成IServiceProvider
        {
            //services.AddSingleton<Person>();//表示把Person注入。BuildAspectCoreServiceProvider是让aspectcore接管注入。在Controller中就可以通过构造函数进行依赖注入了 (一般单一的类才这样单独用)

            RegisterServices(assembly, services);// 把当前程序集传递过去，只要程序集中的public类有方法标记HystrixCommandAttribute特性，就将该类进行注册

            //return services.BuildAspectCoreServiceProvider();//过期方法：由下面方法替代
             return services.BuildDynamicProxyServiceProvider();//表示由AspectCore来接管依赖注入的功能
        }


        private static void RegisterServices(Assembly assembly, IServiceCollection services)
        {
            foreach (Type type in assembly.GetExportedTypes())
            {
                bool hasHystrixCommandAttr = type.GetMethods().Any(m => m.GetCustomAttribute(typeof(HystrixCommandAttribute)) != null);
                if (hasHystrixCommandAttr)
                {
                    services.AddSingleton(type);
                }
            }
        }
    }
}
