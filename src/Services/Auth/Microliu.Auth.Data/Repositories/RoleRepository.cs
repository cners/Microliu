using Microliu.Auth.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(AuthDbContext ctx) : base(ctx) { }

        public Role Add(Role role)
        {
            return _context.role.Add(role).Entity;
        }

        public IQueryable<Role> GetByName(string roleName)
        {
            return GetAll().Where(e => e.RoleName == roleName);
        }
        public IQueryable<Role> GetRoles(SearchRoleModel input)
        {
            var roles = GetAll();
            if (!string.IsNullOrEmpty(input.RoleName))
            {
                roles = roles.Where(e => e.RoleName == input.RoleName);
            }
            return roles.OrderByDescending(e => e.CreateTime)
                .Skip(input.GetSkipValue()).Take(input.PageSize);
        }

        public Role Remove(Role role)
        {
            role.IsDelete = IsDelete.Deleted;
            return _context.Set<Role>().Update(role).Entity;
        }

        public Role Update(Role role)
        {
            return _context.Set<Role>().Update(role).Entity;
        }
    }
}
