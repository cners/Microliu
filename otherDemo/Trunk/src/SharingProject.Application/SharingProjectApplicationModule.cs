using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SharingProject.Authorization;

namespace SharingProject
{
    [DependsOn(
        typeof(SharingProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SharingProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SharingProjectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SharingProjectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
