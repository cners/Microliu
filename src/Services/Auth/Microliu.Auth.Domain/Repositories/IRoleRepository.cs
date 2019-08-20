using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        IQueryable<Role> GetByName(string roleName);
        void UpdateRoleName(string id, string newRoleName);
    }
}
