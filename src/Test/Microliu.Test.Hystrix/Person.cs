using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microliu.Test.Hystrix
{
    public class Person
    {
        [HystrixCommand(nameof(Hello1FallBackAsync), MaxRetryTimes = 3, EnableCircuitBreater = true)]
        public virtual async Task<string> HelloAsync(string name)
        {
            Console.WriteLine($"正常执行： {name}");

            string s = null;
            s.ToString();
            return "ok";
        }

        [HystrixCommand(nameof(Hello2FallBackAsync))]
        public virtual async Task<string> Hello1FallBackAsync(string name)
        {
            Console.WriteLine($"Hello降级1:" + name);
            string s = null;
            s.ToString();
            return "fail_1";
        }

        public virtual async Task<string> Hello2FallBackAsync(string name)
        {
            Console.WriteLine($"Hello降级2:" + name);
            return "fail_2";
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

        [HystrixCommand(nameof(TestFallBack),CacheTTLMilliseconds =3000)]
        public virtual void Test(int i)
        {
            Console.WriteLine("test" + i);
        }

        public virtual void TestFallBack(int i)
        {
            Console.WriteLine("Test" + i);
        }
    }
}
