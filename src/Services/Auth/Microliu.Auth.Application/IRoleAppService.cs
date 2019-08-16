using Microliu.Auth.Domain;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public interface IRoleAppService
    {
        Task CreateRole(Role role);

        Task<object> RemoveRole(object role);

        Task<object> UpdateRole(object role);

        Task<object> QueryRole(object input);
    }
}
