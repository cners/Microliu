using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microliu.Core.Consul.REST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microliu.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IList<object> _orders = new List<object>();

        [HttpGet(nameof(Order))]
        public IActionResult Order()
        {
            _orders.Add(new
            {
                OrderNo = Guid.NewGuid().ToString(),
                Category = "手机维修",
                OrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            // 下单成功，发送短信
            string smsResult = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                RestTemplate rest = new RestTemplate(httpClient);

                var ret1 = rest.GetForEntityAsync("http://SMSService/api/sms/send").Result;
                if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    smsResult = ret1.Body;
                    Console.WriteLine(ret1.Body);
                }
            }
            return Ok(smsResult);
        }
    }
}