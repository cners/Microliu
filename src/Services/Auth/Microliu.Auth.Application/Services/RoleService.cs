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
            Role role = RoleConverter.ToRole(input);
            //role = _roleRepository.Add(role);
            
            await _unitOfWork.Add<Role>(role);
            await _unitOfWork.CommitAsync();
        }

        public async Task<string> RemoveRole(string id, CancellationToken ct = default)
        {
            var role = _roleRepository.GetEntity(id);
            role = _roleRepository.Remove(role);
            await _unitOfWork.CommitAsync();
            return role.Id;
        }

        public async Task<bool> UpdateRoleName(string id, string newRoleName)
        {
            var role = _roleRepository.GetEntity(id);
            role.RoleName = newRoleName;
            role = _roleRepository.Update(role);

            await _unitOfWork.CommitAsync();
            return true;
        }

        public dynamic GetRoles(SearchRoleModel input)
        {
            return _roleRepository.GetRoles(input);
        }
    }
}
