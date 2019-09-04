using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.EmailService.Data.Repositories
{
    public partial class BaseRepository<T> where T : class
    {

        public EmailDbContext _context;

        //public IUnitOfWork UnitOfWork;

        public BaseRepository(EmailDbContext ctx)
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
