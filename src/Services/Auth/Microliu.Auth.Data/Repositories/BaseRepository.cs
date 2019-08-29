using Microliu.Auth.Domain.SeedWork;
using System;
using System.Linq;

namespace Microliu.Auth.DataMySQL
{
    public partial class BaseRepository<T> where T : class
    {

        public AuthDbContext _context;

        //public IUnitOfWork UnitOfWork;

        public BaseRepository(AuthDbContext ctx)
        {
            _context = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetEntity(string id)
        {
            var p = typeof(T).GetProperty("Id");
            return GetAll().Where(e => (p.GetValue(e).ToString()) == id).FirstOrDefault();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

    }
}
