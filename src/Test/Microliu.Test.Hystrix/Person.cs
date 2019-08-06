using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Test.Hystrix
{
    public class Person
    {
        [HystrixCommand(nameof(HelloFallBackAsync))]
        public virtual async Task<string> HelloAsync(string name)
        {
            Console.WriteLine($"hello {name}");

            string s = null;
            s.ToString();
            return "ok";
        }

        public async Task<string> HelloFallBackAsync(string name)
        {
            Console.WriteLine($"执行失败：" + name);
            return "fail";
        }

        [HystrixCommand(nameof(AddFall))]
        public virtual int Add(int a, int b)
        {
            string s = null;
            s.ToString();

            return a + b;
        }

        public int AddFall(int a, int b)
        {
            return 0;
        }
    }
}
