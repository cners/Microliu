

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Microliu.Auth.Domain
{
    public partial class AuthSupervisor : IAuthSuperVisor
    {
        private readonly IEnumerable<IRoleRepository> _roleRepository;


        public AuthSupervisor(IServiceProvider service)
        {
            _roleRepository = service.GetServices<IRoleRepository>();
            //_roleRepository = roleRepository;// 角色
        }
    }
}
