using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SharingProject.Configuration;

namespace SharingProject.Web.Host.Startup
{
    [DependsOn(
       typeof(SharingProjectWebCoreModule))]
    public class SharingProjectWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SharingProjectWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SharingProjectWebHostModule).GetAssembly());
        }
    }
}
