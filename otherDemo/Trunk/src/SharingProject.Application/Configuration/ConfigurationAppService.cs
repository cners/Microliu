using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using SharingProject.Configuration.Dto;

namespace SharingProject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SharingProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
