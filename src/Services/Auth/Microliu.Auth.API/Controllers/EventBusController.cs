using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


// 以下内容仅为测试专用


namespace Microliu.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventBusController : ControllerBase, ICapSubscribe
    {
        private readonly ICapPublisher _publisher;

        public EventBusController(ICapPublisher capPublisher)
        {
            _publisher = capPublisher;
        }


        [HttpGet(nameof(Publish))]
        public IActionResult Publish()
        {
            _publisher.Publish<dynamic>("auth.services.test.publish", new Person { Name = "测试发布内容" });
            return Ok("发布完成");
        }


        [CapSubscribe("auth.services.test.publish")]
        public void ReceivedMessage(Person person)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Controller:"+person.Name);
            Console.ForegroundColor = defaultColor;
        }

    }


    
    public interface ISubscriberService
    {
        void ReceivedMessage(Person person);
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("auth.services.test.publish")]
        public void ReceivedMessage(Person person)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Interface:"+person.Name);
            Console.ForegroundColor = defaultColor;
        }
    }

}