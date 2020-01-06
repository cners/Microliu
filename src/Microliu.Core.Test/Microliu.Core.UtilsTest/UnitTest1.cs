using Microliu.Utils;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Microliu.Core.UtilsTest
{
    public class UnitTest1
    {
        private ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
        }


        [Fact]
        public void MD5()
        {
            string content = "abcdefg";
            var cryptR = CryptHelper.Md5Encrypt(content);
            var securityR = CryptHelper.MD5Encrypt32(content);
            Assert.Equal(cryptR, securityR);
        }
    }
}
