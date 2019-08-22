using Microliu.Auth.Application;
using Microliu.Auth.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microliu.Test.AuthApplication.TestCase
{
    public class UserTest
    {
        private IAuthApplication _authApplication;

        public UserTest()
        {
            _authApplication = ApplicationFactory.GetIAuthApplication();
        }

        public static IEnumerable<object[]> GetCreateUserTestCase()
        {
            //yield return new object[] { new CreateUserModel { NickName = "刘壮", Password = "123456", UserName = "liuzhuang",Email="" } };
            //yield return new object[] { new CreateUserModel { NickName = "小新", Password = "123456", UserName = "xin",Email="xin@lzassist.com" } };
            //yield return new object[] { new CreateUserModel { NickName = "小王", Password = "123456", UserName = "wang" ,Email=""} };
            yield return new object[] { new CreateUserModel { NickName = "小王1", Password = "123456", UserName = "wang1" ,Email=""} };
        }

        [Theory]
        [MemberData(nameof(GetCreateUserTestCase))]
        public async Task CreateUserAsync(CreateUserModel input)
        {
            bool result =await _authApplication.CreateUser(input);
            Assert.False(result);
            //Assert.Equal(expected, result);

          
        }

   
    }
}
