using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AuthDbContext ctx) : base(ctx) { }

        public IQueryable<User> Query(string id, string positionId)
        {
            return _context.user.Where(e => e.UserPositions.Where(up => up.PositionId == positionId).Any());
        }
        public new User GetEntity(string id)
        {
            return GetAll().Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Exists(string userName)
        {
            return GetAll().Where(e => e.UserName.ToLower() == userName.ToLower()).Count() > 0;
        }

        public User Add(User user)
        {
            return _context.user.Add(user).Entity;
        }
    }
}
