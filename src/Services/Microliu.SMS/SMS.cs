using Microliu.Core.Hystrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.SMS
{

    public class SMS
    {
        [HystrixCommand(nameof(SendFallBackAsync))]
        public virtual async Task<string> SendAsync(string message)
        {
            Console.WriteLine($"准备发送短信：{ message}");

            string s = null;
            s.ToString();
            return "发送成功";
        }

        public async Task<string> SendFallBackAsync(string message)
        {
            Console.WriteLine("降级1");
            return "降级发送成功-Level1";
        }
    }
}
