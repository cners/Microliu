using Microliu.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Microliu.Core.UtilsTest
{
    public class VerifyHelperTest
    {
        [Theory]
        [InlineData("liu.zhuang@a.lynxons.com",true)]
        [InlineData("liu.zhuang@6iuu.com",true)]
        [InlineData("liuzhuang@6iuu.com",true)]
        [InlineData("980462345@qq.com",true)]
        [InlineData("liuzhuangs@hotmail.com",true)]
        [InlineData("a.com",false)]
        [InlineData("adac@a.b.c.com",true)]
        [InlineData("adac@a.b.c.d.com",true)]
        public void IsEmail(string email,bool expected)
        {
            var isEmail = VerifyHelper.IsEmail(email);
            Assert.Equal(expected, isEmail);
        }


        [Theory]
        [InlineData("123",true)]
        [InlineData("12345678",true)]
        [InlineData("1234567890",true)]
        [InlineData("16619720393",true)]
        [InlineData(null,false)]
        [InlineData("not a number",false)]
        [InlineData("1d3",false)]
        [InlineData("0x11",false)]
        public void IsNumber(string num,bool expected)
        {
            var r = VerifyHelper.IsNumber(num);
            Assert.Equal(expected, r);
        }


        [Theory]
        [InlineData("330781198509079591", true)]
        [InlineData("130630199909122116", true)]
        [InlineData("44122520011021581X", true)]
        [InlineData("410802199603310053", true)]
        [InlineData("130282198909015118", true)]
        [InlineData("350301199908101850", true)]
        [InlineData("330781198509074432", true)]
        [InlineData("330781198509071434", true)]
        [InlineData("33078119850907144", false)]
        [InlineData("41092619950725207X", true)]
        public void IsIdCard(string idCard,bool expected)
        {
            var r = VerifyHelper.IsIdCard(idCard);
            Assert.Equal(expected, r);
        }

        [Theory]
        [InlineData("16619720393",true)]
        [InlineData("15090246296",true)]
        [InlineData("17703930725",true)]
        [InlineData("19912346579",true)]
        [InlineData("1",false)]
        [InlineData("",false)]
        [InlineData(null,false)]
        [InlineData("0123456789",false)]
        [InlineData("06619720393",false)]
        [InlineData("166197203933",false)]
        public void IsTel(string tel ,bool expected)
        {
            var r = VerifyHelper.IsTel(tel);
            Assert.Equal(expected, r);
        }
    }
}
