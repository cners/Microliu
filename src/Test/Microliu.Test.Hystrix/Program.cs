using AspectCore.DynamicProxy;
using System;
using System.Threading;

namespace Microliu.Test.Hystrix
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            using (IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build())
            {
                Person p = proxyGenerator.CreateClassProxy<Person>();
                Console.WriteLine(p.HelloAsync("First").Result);
                //var h = p.HelloAsync("da").Result;
                //p.Test(10);
                Console.WriteLine(p.Add(1, 2));
                while (true)
                {
                    Console.WriteLine(p.HelloAsync("hello world").Result);
                    Thread.Sleep(100);
                }
            }

            Console.ReadLine();
        }
    }
}
