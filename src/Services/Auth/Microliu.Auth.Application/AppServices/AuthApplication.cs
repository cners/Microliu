using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.Application
{
    public partial class AuthApplication : IAuthApplication
    {
        private readonly IServiceProvider _services;
        private IUnitOfWork _unitOfWork;

        private readonly IRoleRepository _roleRepos;
        private readonly IUserRepository _userRepos;
        private readonly IPositionRepository _position;

        public AuthApplication(IServiceProvider services)
        {
            _services = services;

            //_mssqlRoleRepos = services.GetServices<IRoleRepository>(DbType.SQLServer);
            //_mysqlRoleRepos = services.GetServices<IRoleRepository>(DbType.MySQL);

            _unitOfWork = services.GetService<IUnitOfWork>();

            _roleRepos = services.GetService<IRoleRepository>();
            _userRepos = services.GetService<IUserRepository>();
            _position = services.GetService<IPositionRepository>();
        }

      
    }
}
