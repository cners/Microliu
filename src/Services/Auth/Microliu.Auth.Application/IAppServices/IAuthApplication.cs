using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial interface IAuthApplication
    {
        // 角色
        Task CreateRole(CreateRoleModel input, CancellationToken ct = default(CancellationToken));

        Task<string> RemoveRole(string id, CancellationToken ct = default(CancellationToken));

        Task<bool> UpdateRoleName(string id, string newRoleName);


        // 岗位
        Task CreatePosition(CreatePositionModel input, CancellationToken ct = default(CancellationToken));
        dynamic GetPosition(string id);

        dynamic GetPositions();

        dynamic GetPositions(SearchPositionModel input, CancellationToken ct = default(CancellationToken));
        // 用户岗位
        Task SetUserPosition(string userId, string positionId, CancellationToken ct = default(CancellationToken));

      

        // 角色
        dynamic GetRoles(SearchRoleModel input);
    }
}
