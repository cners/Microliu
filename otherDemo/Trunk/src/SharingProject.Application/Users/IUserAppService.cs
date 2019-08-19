using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SharingProject.Roles.Dto;
using SharingProject.Users.Dto;

namespace SharingProject.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
