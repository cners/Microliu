using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public partial class AuthSupervisor : IAuthSuperVisor
    {
        public async Task CreateRole(Role role, CancellationToken ct = default(CancellationToken))
        {
            foreach (var rs in _roleRepository)
            {
                await rs.AddAsync(role, ct);
                await rs.SaveChangesAsync();
            }
           
        }
    }
}
