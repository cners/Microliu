using System.IO;
using System.Net;
using System.Text;

namespace Microliu.Utils
{
    public class HttpHelper
    {
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        public static string Post(string requestUrl, string parameters)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(parameters);
            httpWebRequest.ContentLength = data.Length;
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream getStream = httpWebResponse.GetResponseStream();
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(getStream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
