using Microliu.Auth.Application;
using Microliu.Auth.DataMySQL;
using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Repositories;
using Microsoft.AspNetCore;
using System;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Microliu.Test.AuthApplication
{
    public class UnitTest1
    {
        private IAuthApplication _authApplication;
        public UnitTest1()
        {
            //var container = new UnityContainer();
            //container.RegisterType<IDbContext, AuthDbContext>();
            //container.RegisterType<IUnitOfWork, UnitOfWork>();
            //container.RegisterType<IAuthApplication, Microliu.Auth.Application.AuthApplication>();
            //container.RegisterType<IRoleRepository, RoleRepository>();
            //container.RegisterType<IUserRepository, UserRepository>();


            //_authApplication = container.Resolve<IAuthApplication>();

           
        }

        [Fact]
        public async Task CreatePositon()
        {

            for (int i = 0; i < 5; i++)
            {
                await _authApplication.CreatePosition();
            }
        }
    }
}
