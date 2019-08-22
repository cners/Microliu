using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext, IUnitOfWork unitOfWork, AuthDbContext authDbContext)
            : base(dbContext, unitOfWork, authDbContext)
        {
        }

        public IQueryable<User> Query(string id, string positionId)
        {
            return _authDbContext.user.Where(e => e.UserPositions.Where(up => up.PositionId == positionId).Any());
        }
        public new User GetEntity(string id)
        {
            return _entities.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Exists(string userName)
        {
            return _entities.Where(e => e.UserName.ToLower() == userName.ToLower()).Count() > 0;
        }
    }
}
