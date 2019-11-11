using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microliu.Test.AuthApplication
{
    public class RoleTest
    {
        private IAuthService _authApplication;
        private ITestOutputHelper _output;
        public RoleTest(ITestOutputHelper outputHelper)
        {
            _authApplication = ApplicationFactory.GetIAuthApplication();
            _output = outputHelper;
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


        public static IEnumerable<object[]> GetRolesTestCase()
        {
            yield return new object[] { new SearchRoleModel { PageIndex = 0, PageSize = 2 } };// 第一次慢
            yield return new object[] { new SearchRoleModel { PageIndex = 1, PageSize = 1 } };
            yield return new object[] { new SearchRoleModel { PageIndex = 1, PageSize = 2 } };

        }

        [Theory]
        [MemberData(nameof(GetRolesTestCase))]
        // MemberData 传递参数可参考：https://juejin.im/post/5a541ae6f265da3e4e257c8c
        public void GetRoles(SearchRoleModel input)
        {
            var roles = _authApplication.GetRoles(input);

            foreach (var role in roles)
            {
                foreach (var prop in role.GetType().GetProperties())
                {
                    _output.WriteLine($"{prop.Name}= {prop.GetValue(role).ToString()}");
                }
                _output.WriteLine("");
            }
        }

    }
}
