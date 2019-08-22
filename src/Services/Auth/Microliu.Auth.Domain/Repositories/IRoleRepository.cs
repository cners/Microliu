using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        IQueryable<Role> GetByName(string roleName);

        IQueryable<Role> GetRoles(SearchRoleModel input);
    }
}
