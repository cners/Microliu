using Microliu.Core.RedisCache;
using Microliu.Core.RedisTest.Container;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microliu.Core.RedisTest
{
    public class RedisTestCase
    {
        private ICacheService _cache;
        private ILogger _logger;
        private ITestOutputHelper _output;

        public RedisTestCase(ITestOutputHelper outputHelper)
        {
            _cache = RedisFactory.Get();
            _output = outputHelper ?? throw new ArgumentNullException(nameof(outputHelper));
            //_logger = RedisFactory.GetLogger();
        }


        [Theory]
        [InlineData("test", "testValue")]
        public void SetString(string key, string value)
        {
            _cache.StringSet(key ?? "", value ?? "");
            //_logger.LogInformation("insert key={key}, value={value}", key ?? "", value ?? "");
        }

        [Fact]
        public void SetStringFor()
        {
            //for (int j = 0; j < 20000; j++)
            //{   
            //    _cache.StringSet(j.ToString()+"second", j.ToString());
            //}
        }


        [Fact]
        public void SetInt()
        {
            // _cache.StringSet<long>("a", 0);
            //_cache.StringIncrement("a");
            _output.WriteLine($"{_cache.StringGet("a")}");
            var num = _cache.StringDecrement("a", 2);
            _output.WriteLine(num.ToString());
            //_cache.StringIncrement("a", 2);
        }

        [Fact]
        public void SetHash()
        {
            //var dic = new Dictionary<string, Dictionary<string,string>>();

            var dicData = new Dictionary<string, string>();
            dicData.TryAdd("name", "LiuZhuang");
            dicData.TryAdd("age", "25");
            dicData.TryAdd("gender", "male");

            dicData = new Dictionary<string, string>();
            dicData.TryAdd("name", "LiuZhuang2");
            dicData.TryAdd("age", "25");
            dicData.TryAdd("gender", "male");

            dicData = dicData.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            _cache.HashSet("userinfo", dicData);
            //_cache.KeyExpire("userinfo", TimeSpan.FromSeconds(10));
        }


        [Fact]
        public void GetHash()
        {
            var hashKey = _cache.HashGetAll("userinfo");
            var json = JsonConvert.SerializeObject(hashKey);
            var userinfo = JsonConvert.DeserializeObject<UserInfo>(json);
            _output.WriteLine(json);
        }

        [Theory]
        [InlineData("userinfo", "age")]
        public void DeleteHash(string key, string field)
        {
            if (_cache.HashKeyExists(key, field))
            {
                var result = _cache.HashDelete(key, field);
                _output.WriteLine(result.ToString());
            }
            else
            {
                _output.WriteLine("Not Exists!!!");
            }
        }

        [Theory]
        [InlineData("userinfo")]
        public void HashKeys(string key)
        {
            var keys = _cache.HashKeys(key);
            _output.WriteLine(string.Join(",", keys));
        }


    }

    public class UserInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

}
