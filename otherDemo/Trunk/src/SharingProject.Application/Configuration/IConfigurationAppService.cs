using System.Threading.Tasks;
using SharingProject.Configuration.Dto;

namespace SharingProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
