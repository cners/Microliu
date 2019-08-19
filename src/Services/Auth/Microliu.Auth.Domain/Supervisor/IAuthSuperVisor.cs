using Microliu.Auth.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public interface IAuthSuperVisor
    {
        Task CreateRole(CreateRoleModel input, CancellationToken ct = default(CancellationToken));

        Task<bool> RemoveRole(string id, CancellationToken ct = default(CancellationToken));
    }
}
