using Microliu.Auth.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private readonly AuthContextMySQL _context;

        public RoleRepository(AuthContextMySQL context)
        {
            _context = context;
        }


        public async Task<Role> AddAsync(Role newEntity, CancellationToken ct = default)
        {
            await _context.role.AddAsync(newEntity);
            //await _context.SaveChangesAsync();
            return newEntity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task RemoveAsync(string id, CancellationToken ct = default)
        {
            var role = _context.role.FirstOrDefault(e => e.Id == id && e.IsDeleted == 1);
            role.IsDeleted = -1;
            _context.role.Update(role);
        }
    }
}
