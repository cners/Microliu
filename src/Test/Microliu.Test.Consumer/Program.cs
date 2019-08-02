using Consul;
using RestTemplateCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Microliu.Test.Consumer
{
    class Program
    {
        static string findServiceName = "SMS";
        static string consulPort = "8500";
        static string consulAddress = "http://127.0.0.1";
        static void Main(string[] args)
        {


            using (var client = new ConsulClient(c => c.Address = new Uri($"{consulAddress}:{consulPort}")))
            {

                var services = client.Agent.Services().Result.Response.Values
                                           .Where(s => s.Service.Equals(findServiceName, StringComparison.OrdinalIgnoreCase));
                //var nodes = consulClient.Agent.GetNodeName().Result;

                //foreach (var service in services.Values)
                //{
                //    Console.WriteLine($"enable={service.EnableTagOverride}, id={service.ID}, name={service.Service}, ip={service.Address}, port={service.Port}");
                //}
                if (!services.Any())
                {
                    Console.WriteLine($"未发现服务 [{findServiceName}]");
                }
                else
                {

                    var index = Environment.TickCount % services.Count();
                    var service = services.ElementAt(index);
                    Console.WriteLine($"{service.Address}:{service.Port} - {service.EnableTagOverride}");


                    Console.WriteLine(d.Response); ;
                }
            }

            //using (HttpClient httpClient = new HttpClient())
            //{
            //    RestTemplate rest = new RestTemplate(httpClient);

            //    var ret1 = rest.GetForEntityAsync<string>("http://SMS/api/healthcheck/").Result;
            //    Console.WriteLine(ret1.StatusCode);
            //    if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        Console.WriteLine(ret1.Body);
            //    }
            //}

            Console.ReadLine();
        }

        static AgentService FindService(string serviceName)
        {
            var services = new List<AgentService>();
            using (var client = new ConsulClient(c => c.Address = new Uri($"{consulAddress}:{consulPort}")))
            {
                var servicesRunning = client.Health.Service(serviceName).Result.Response;
                foreach (var item in servicesRunning)
                {
                    foreach (var check in item.Checks)
                    {
                        if (check.Status == HealthStatus.Passing)
                        {
                            services.Add(item.Service);
                        }
                    }
                }
            }
            var index = Environment.TickCount % services.Count();
            var service = services.ElementAt(index);
            return service;
        }
    }
}
