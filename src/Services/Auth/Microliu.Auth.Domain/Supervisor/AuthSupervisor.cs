

using Microliu.Auth.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Microliu.Auth.Domain
{
    public partial class AuthSupervisor : IAuthSuperVisor
    {
        //private readonly IEnumerable<IRoleRepository> _roleRepository;

        // 满足多种数据库同时使用
        //private readonly IRoleRepository _mssqlRoleRepos;
        //private readonly IRoleRepository _mysqlRoleRepos;


        private readonly IRoleRepository _roleRepos;
        private readonly IServiceProvider _services;

        public AuthSupervisor(IServiceProvider services)
        {
            _services = services;

            //_mssqlRoleRepos = services.GetServices<IRoleRepository>(DbType.SQLServer);
            //_mysqlRoleRepos = services.GetServices<IRoleRepository>(DbType.MySQL);

            _roleRepos = services.GetService<IRoleRepository>();
        }

    }
}


