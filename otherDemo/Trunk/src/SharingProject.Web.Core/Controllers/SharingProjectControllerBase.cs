using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SharingProject.Controllers
{
    public abstract class SharingProjectControllerBase: AbpController
    {
        protected SharingProjectControllerBase()
        {
            LocalizationSourceName = SharingProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
