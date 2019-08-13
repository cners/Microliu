using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public interface IAuthSuperVisor
    {

        Task CreateRole(Role role, CancellationToken ct = default(CancellationToken));
    }
}
