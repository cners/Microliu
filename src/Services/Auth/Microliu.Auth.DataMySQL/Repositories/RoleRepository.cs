using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        //private readonly AuthDbContext _context;

        public RoleRepository(IDbContext context, IUnitOfWork unitOfWork, AuthDbContext authDbContext) 
            : base(context,unitOfWork,authDbContext)
        {
        }

     

        public IQueryable<Role> Get(string id)
        {
            return _entities.Where(x => x.Id == id && x.IsDeleted == 1);
        }


        public IQueryable<Role> GetByName(string roleName)
        {
            return _entities.Where(e => e.RoleName == roleName);
        }

        public void UpdateRoleName(string id, string newRoleName)
        {
            throw new System.NotImplementedException();
        }
    }
}
