using Autofac;
using Microsoft.Extensions.Configuration;

namespace Microliu.Utils.Module
{
    public enum ModuleExecPriority
    {
        /// <summary>
        /// execute first
        /// </summary>
        Critical = 1,
        /// <summary>
        /// after Critical
        /// </summary>
        Important = 2,

        /// <summary>
        /// after Important
        /// </summary>
        Normal = 4,
        /// <summary>
        /// last
        /// </summary>
        Low = 3
    }
    public abstract class ModuleBase
    {
        public virtual ModuleExecPriority Priority => ModuleExecPriority.Important;
        protected IConfigurationRoot MicroliuAppSettings { get; }

        public ModuleBase(IConfigurationRoot configurationRoot)
        {
            MicroliuAppSettings = configurationRoot;
        }

        /// <summary>
        /// Microliu 注册时触发
        /// </summary>
        /// <param name="containerBuilder"></param>
        public virtual void DoRegister(ContainerBuilder containerBuilder)
        {

        }

        /// <summary>
        /// Microliu初始化
        /// </summary>
        /// <param name="container"></param>
        public virtual void DoInit(IContainer container)
        {

        }


        /// <summary>
        /// Microliu运行中
        /// </summary>
        /// <param name="container"></param>
        public virtual void DoRun(IContainer container)
        {

        }
        /// <summary>
        /// Microliu运行前
        /// </summary>
        /// <param name="container"></param>
        public virtual void DoBeforeRun(IContainer container)
        {

        }

    }
}
