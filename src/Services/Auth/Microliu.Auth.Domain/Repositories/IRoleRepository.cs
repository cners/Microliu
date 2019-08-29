using Microliu.Auth.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role Add(Role role);

        Role Remove(Role role);

        Role Update(Role role);

        IQueryable<Role> GetByName(string roleName);

        IQueryable<Role> GetRoles(SearchRoleModel input);
    }
}
