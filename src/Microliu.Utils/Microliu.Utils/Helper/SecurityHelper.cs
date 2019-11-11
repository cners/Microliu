using System.Security.Cryptography;
using System.Text;

namespace Microliu.Utils
{
    public class SecurityHelper
    {
        /// <summary>
        /// 32位MD5加密字符串
        /// </summary>
        /// <param name="content">待加密的明文内容</param>
        /// <returns></returns>
        public static string MD5Encrypt32(string content)
        {
            string encrypted = "";
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            for (int i = 0; i < buffer.Length; i++)
            {
                encrypted += buffer[i].ToString("X");
            }
            return encrypted;
        }
    }
}
