using Autofac;
using Microsoft.Extensions.Configuration;

namespace Microliu.Utils.Module
{
    public class ServerModuleBase : ModuleBase
    {

        public ServerModuleBase(IConfigurationRoot jimuAppSettings) : base(jimuAppSettings)
        {
        }

        /// <summary>
        /// 当自定义服务注册时
        /// </summary>
        /// <param name="serviceContainerBuilder"></param>
        /// <param name="jimuAppSettings"></param>
        public virtual void DoServiceRegister(ContainerBuilder serviceContainerBuilder)
        {

        }

        /// <summary>
        /// 当自定义服务初始化时
        /// </summary>
        /// <param name="container"></param>
        public virtual void DoServiceInit(IContainer container)
        {

        }
    }
}
