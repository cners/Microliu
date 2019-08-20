using Microliu.Auth.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public interface IAuthApplication
    {

        Task CreateRole(CreateRoleModel input, CancellationToken ct = default(CancellationToken));

        Task<string> RemoveRole(string id, CancellationToken ct = default(CancellationToken));

        Task<bool> UpdateRoleName(string id, string newRoleName);


        // 岗位
        Task CreatePosition(CancellationToken ct = default(CancellationToken));
        dynamic GetPosition(string id);
        // 用户岗位
        Task SetUserPosition(string userId,string positionId,CancellationToken ct = default(CancellationToken));

        // 用户
        dynamic GetUser(string id);

        dynamic GetUsers(string positionId);

        Task<string> CreateUser(CreateUserModel input);
    }
}
