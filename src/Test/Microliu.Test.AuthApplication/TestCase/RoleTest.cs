using Microliu.Auth.Application;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Microliu.Test.AuthApplication
{
    public class RoleTest
    {
        private IAuthApplication _authApplication;
        public RoleTest()
        {
            _authApplication = ApplicationFactory.GetIAuthApplication();
        }

        [Fact]
        public async Task CreateRole()
        {
            var createRole = new CreateRoleModel
            {
                CreatorId = "liu",
                RoleName = Guid.NewGuid().ToString("N").Substring(10, 7),
                Sort = 100
            };
            await _authApplication.CreateRole(createRole);
        }
    }
}
