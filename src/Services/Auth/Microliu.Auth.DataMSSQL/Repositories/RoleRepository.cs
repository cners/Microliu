using Microliu.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMSSQL
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthContext _context;

        public RoleRepository(AuthContext context)
        {
            _context = context;
        }


        public async Task<Role> AddAsync(Role newEntity, CancellationToken ct = default)
        {
            _context.role.Add(newEntity);
            await _context.SaveChangesAsync();
            return newEntity;
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
