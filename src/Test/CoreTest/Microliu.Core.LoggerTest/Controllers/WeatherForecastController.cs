using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microliu.Core.LoggerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger;
        //https://www.cnblogs.com/alunchen/p/9988564.html

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            //_logger.LogDebug("进入构造函数2", "构造函数", "刘壮");

            //ExceptionlessClient.Default.CreateLog("进入构造函数").Submit();

            _logger.LogError($"[{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [Error] [人为错误]");
            _logger.LogError($"[{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [正常] [错误信息]");


            //_logger.LogWarning($"[warning]");
            //try
            //{
            //    throw new Exception("exception for debug");
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(new EventId().Id, ex, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [Error] [自定义异常]", "exxxx");
            //}
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
