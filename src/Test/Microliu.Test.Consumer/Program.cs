using Consul;
using Polly;
using RestTemplateCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace Microliu.Test.Consumer
{
    class Program
    {
        static string findServiceName = "BizLogger";
        static string consulPort = "8500";
        static string consulAddress = "http://127.0.0.1";
        static void Main(string[] args)
        {


            //using (var client = new ConsulClient(c => c.Address = new Uri($"{consulAddress}:{consulPort}")))
            //{

            //    var services = client.Agent.Services().Result.Response.Values
            //                               .Where(s => s.Service.Equals(findServiceName, StringComparison.OrdinalIgnoreCase));
            //    //var nodes = consulClient.Agent.GetNodeName().Result;

            //    //foreach (var service in services.Values)
            //    //{
            //    //    Console.WriteLine($"enable={service.EnableTagOverride}, id={service.ID}, name={service.Service}, ip={service.Address}, port={service.Port}");
            //    //}
            //    if (!services.Any())
            //    {
            //        Console.WriteLine($"未发现服务 [{findServiceName}]");
            //    }
            //    else
            //    {

            //        var index = Environment.TickCount % services.Count();
            //        var service = services.ElementAt(index);
            //        Console.WriteLine($"{service.Address}:{service.Port} - {service.EnableTagOverride}");

            //    }
            //}

            // RPC Demo

            //using (HttpClient httpClient = new HttpClient())
            //{
            //    RestTemplate rest = new RestTemplate(httpClient);

            //    var ret1 = rest.GetForEntityAsync("http://BizLogger/api/bizlogger/getlogs/").Result;
            //    Console.WriteLine(ret1.StatusCode);
            //    if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        Console.WriteLine(ret1.Body);
            //    }
            //}

            // Polly Demo
            // https://www.cnblogs.com/wyt007/p/9197987.html
            Policy policy = Policy.Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(3));

            while (true)
            {
                Console.WriteLine("开始执行");
                try
                {
                    policy.Execute(() =>
                    {
                        Console.WriteLine("Begin..................");
                        throw new Exception("error");
                        Console.WriteLine("Done...............");
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("异常了");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Thread.Sleep(500);
            }
            Console.ReadLine();
        }

    }
}
