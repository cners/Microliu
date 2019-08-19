using Microliu.Auth.Domain.Converters;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Domain
{
    public partial class AuthSupervisor : IAuthSuperVisor
    {
        public async Task CreateRole(CreateRoleModel input, CancellationToken ct = default(CancellationToken))
        {
            //foreach (var rs in _roleRepository)
            //{
            //    await rs.AddAsync(role, ct);
            //    await rs.SaveChangesAsync();
            //}

            // 第一次尝试获取SQLserver RoleRepository
            //var sqlserverRoleRepos = _roleRepository.Where(r => r.GetDbType() == DbType.SQLServer).FirstOrDefault();
            //await sqlserverRoleRepos.AddAsync(role, ct);
            //await sqlserverRoleRepos.SaveChangesAsync();


            // 第二次尝试获取SQLserver RoleRepository，优化了IServiceProvider扩展
            //var sqlserverRoleRepos = _services.GetServices<IRoleRepository>(DbType.SQLServer);
            //var role = RoleConverter.ToRole(input);
            //role.RoleName = "存入mysql";
            //await _mssqlRoleRepos.AddAsync(role, ct);
            //await _mssqlRoleRepos.SaveChangesAsync();

            //role.RoleName = "同时存入mysql";
            //await _mysqlRoleRepos.AddAsync(role, ct);
            //await _mysqlRoleRepos.SaveChangesAsync();

            var role = RoleConverter.ToRole(input);
            await _roleRepos.AddAsync(role, ct);
            await _roleRepos.SaveChangesAsync();
        }

        public async Task<bool> RemoveRole(string id, CancellationToken ct = default(CancellationToken))
        {
            await _roleRepos.RemoveAsync(id, ct);
            await _roleRepos.SaveChangesAsync();
            return true;
        }
    }
}
