using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(IDbContext context, IUnitOfWork unitOfWork, AuthDbContext authDbContext)
            : base(context, unitOfWork, authDbContext)
        {
        }


        public IQueryable<Role> GetByName(string roleName)
        {
            return _entities.Where(e => e.RoleName == roleName);
        }
        public IQueryable<Role> GetRoles(SearchRoleModel input)
        {
            if (!string.IsNullOrEmpty(input.RoleName))
            {
                _entities = _entities.Where(e => e.RoleName == input.RoleName);
            }
            return _entities.OrderByDescending(e => e.CreateTime)
                .Skip(input.GetSkipValue()).Take(input.PageSize);
        }
    }
}
