using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Microliu.Utils
{
    public class VerifyHelper
    {

        /// <summary>
        /// 是否为合法格式的邮箱地址
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            //①([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)
            //②\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
            //③^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$
            return Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", email);
        }


        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
        /// </summary>
        /// <param name="text">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumber(string text)
        {
            return VerifyHelper.Regex("^[1-9]*[0-9]*$", text);
        }

        /// <summary>
        /// 是否为手机号
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public static bool IsTel(string tel)
        {
            tel = tel ?? "";
            return Regex(@"^1[3456789]\d{9}$", tel);
        }

        /// <summary>
        /// 是否为电话号码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhone(string phone)
        {
            return Regex(@"^(\d{3,4}-)?\d{6,8}$", phone);
        }

        /// <summary>
        /// 是否为身份证号
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static bool IsIdCard(string idCard)
        {
            //11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"
            return Regex(@"(~\d{15})|(^\d{17}(\d|X|x)$)", idCard);
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="express">正则表达式的内容。</param>
        /// <param name="text">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool Regex(string express, string text)
        {
            if (text == null) return false;
            Regex myRegex = new Regex(express);
            if (text.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(text);
        }
    }
}
