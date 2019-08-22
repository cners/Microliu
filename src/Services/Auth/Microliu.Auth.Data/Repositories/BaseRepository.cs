using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Infrastructure;
using System.Linq;

namespace Microliu.Auth.DataMySQL
{
    public partial class BaseRepository<T> where T : class
    {
        public DbType GetDbType()
        {
            return DbType.MySQL;//需要优化
        }

        public IQueryable<T> _entities;

        public readonly IUnitOfWork _unitOfWork;

        public readonly IDbContext _dbContext;

        public AuthDbContext _authDbContext;

        public BaseRepository(IDbContext dbContext, IUnitOfWork unitOfWork, AuthDbContext authDbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
            _unitOfWork = unitOfWork;
            _authDbContext = authDbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public T GetEntity(string id)
        {
            var p = typeof(T).GetProperty("Id");
            return _entities.Where(e => (p.GetValue(e).ToString()) == id).FirstOrDefault();
        }

        public void Dispose()
        {
            if (_authDbContext != null)
            {
                _authDbContext.Dispose();
            }
        }

    }
}
