using Microliu.Auth.Domain;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public class RoleAppService : IRoleAppService
    {
        private IAuthSuperVisor _authSuperVisor;
        public RoleAppService(IAuthSuperVisor authSuperVisor)
        {
            _authSuperVisor = authSuperVisor;
        }
        public async Task CreateRole(CreateRoleModel role)
        {
            await _authSuperVisor.CreateRole(role);
        }

        public async Task<object> QueryRole(object input)
        {
            return "QueryRole";
        }

        public async Task<bool> RemoveRole(string id)
        {
            var removeResult = await _authSuperVisor.RemoveRole(id);
            return removeResult;
        }

        public async Task<object> UpdateRole(object role)
        {
            return "UpdateRole";
        }
       
    }
}
