using Microliu.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Microliu.Core.UtilsTest
{
    public class StringHelperTest
    {

        [Theory]
        [InlineData("liuzhuang@6iuu.com","6iuu.com")]
        [InlineData("liu.zhuang@a.lynxons.com", "a.lynxons.com")]
        [InlineData("980462345@qq.com", "qq.com")]
        [InlineData("liuzhuangs@hotmail.com", "hotmail.com")]
        [InlineData("a.com", "")]
        [InlineData("adac@a.b.c.com", "a.b.c.com")]
        [InlineData("adac@a.b.c.d.com", "a.b.c.d.com")]
        [InlineData(null,"")]
        public void GetEmailDomain(string email,string expected)
        {
            string domain = StringHelper.GetEmailDomain(email);
            Assert.Equal(expected, domain);
        }
    }
}
