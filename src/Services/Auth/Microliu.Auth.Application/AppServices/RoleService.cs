using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Converters;
using Microliu.Auth.Domain.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial class AuthApplication
    {

        public async Task CreateRole(CreateRoleModel input, CancellationToken ct = default)
        {
            var role = RoleConverter.ToRole(input);
            await _unitOfWork.RegisterNew<Role>(role);
            await _unitOfWork.CommitAsync();
        }

        public async Task<string> RemoveRole(string id, CancellationToken ct = default)
        {
            var role = _roleRepos.GetEntity(id);
            role.IsDelete =  IsDelete.Deleted;
            await _unitOfWork.RegisterUpdate<Role>(role);
            await _unitOfWork.CommitAsync();
            return role.Id;
        }

        public async Task<bool> UpdateRoleName(string id, string newRoleName)
        {
            var role = _roleRepos.GetEntity(id);
            role.RoleName = newRoleName;
            await _unitOfWork.RegisterUpdate<Role>(role);
            return await _unitOfWork.CommitAsync();
        }

        public dynamic GetRoles(SearchRoleModel input)
        {
            return _roleRepos.GetRoles(input);
        }
    }
}
