using Microliu.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public class AuthAppService : IAuthAppService
    {
        private IAuthSuperVisor _authSuperVisor;
        public AuthAppService(IAuthSuperVisor authSuperVisor)
        {
            _authSuperVisor = authSuperVisor;
        }
        public async Task CreateRole(Role role)
        {
            await _authSuperVisor.CreateRole(role);
        }

        public async Task<object> QueryRole(object input)
        {
            return "QueryRole";
        }

        public async Task<object> RemoveRole(object role)
        {
            return "RemoveRole";
        }

        public async Task<object> UpdateRole(object role)
        {
            return "UpdateRole";
        }
    }
}
