using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Utils
{
    public interface IApplicationBuilder<out T> where T : class
    {
        /// <summary>
        /// 手动注册Microliu 框架autoface服务，尤其是不能注入的自定义服务
        /// </summary>
        /// <param name="moduleRegister"></param>
        /// <returns></returns>
        T AddModule(Action<ContainerBuilder> moduleRegister);

        /// <summary>
        /// delegate will be execute in initializing host
        /// </summary>
        /// <param name="initializer">autofac IContainer</param>
        /// <returns></returns>
        T AddInitializer(Action<IContainer> initializer);

        T AddBeforeRunner(Action<IContainer> beforeRunner);
        T AddRunner(Action<IContainer> runner);

        /// <summary>
        ///     build serviceHost
        /// </summary>
        /// <returns></returns>
        IApplication Build();
    }
}
