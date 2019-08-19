using Microliu.Auth.Domain;
using Microliu.Auth.Domain.ViewModels;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public interface IRoleAppService
    {
        Task CreateRole(CreateRoleModel role);

        Task<bool> RemoveRole(string id);

        Task<object> UpdateRole(object role);

        Task<object> QueryRole(object input);
    }
}
