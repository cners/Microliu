using Microliu.Core.Redis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//https://www.cnblogs.com/wangyulong/p/8656215.html


namespace Microliu.Core.RedisCache
{
    public class CacheService : ICacheService
    {
        private RedisOptions _options;
        private ILogger<CacheService> _logger;

        /// <summary>
        /// Redis连接池
        /// </summary>
        private BlockingQueue<ConnectionMultiplexer> _redisPool;


        public CacheService(RedisOptions options, ILogger<CacheService> logger)
        {
            _options = options;
            _logger = logger;

            Startup();
        }

        private ConnectionMultiplexer GetRedisConnection()
        {
            return _redisPool.DeQueue(_options.QueueWait);
        }

        #region 字符串操作

        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            if (!_options.Enabled) return string.Empty;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringGet(key);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取存储在键上的字符串的子字符串
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string StringGet(string key, int start, int end)
        {
            if (!_options.Enabled) return string.Empty;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringGetRange(key, start, end);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 设置键的字符串值并返回旧值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string StringGetSet(string key, string value)
        {
            if (!_options.Enabled) return string.Empty;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringGetSet(key, value);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 返回在键处存储的字符串值编译处的位值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool StringGetBit(string key, long offset)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringGetBit(key, offset);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取所有给定键的值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<string> StringMultiGet(params string[] keys)
        {
            List<string> list = new List<string>();
            if (!_options.Enabled) return list;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    foreach (string key in keys)
                    {
                        list.Add(redis.GetDatabase(_options.StorageIndex).StringGet(key));
                    }
                }
                return list;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 存储在键上的字符串值中设置或清除偏移处的位
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool StringSetBit(string key, long offset)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringSetBit(key, offset, true);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 设置键和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringSet(key, value);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }


        /// <summary>
        /// 设置键和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan expiry)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringSet(key, value, expiry);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 设置键和值，仅当键不存在时
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        public void StringSetIfNull(string key, string value, TimeSpan expiry)
        {
            if (!_options.Enabled) return;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    if (redis.GetDatabase(_options.StorageIndex).StringGet(key) == RedisValue.Null)
                    {
                        redis.GetDatabase(_options.StorageIndex).StringSet(key, value, expiry);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取存储键中的值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long StringSize(string key)
        {
            if (!_options.Enabled) return -1;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringLength(key);
                }
                else
                {
                    return -1;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 批量设置键和值
        /// </summary>
        /// <param name="keyValues"></param>
        public void StringMultiSet(Dictionary<string, string> keyValues)
        {
            foreach (var item in keyValues)
            {
                if (!string.IsNullOrEmpty(item.Key))
                    StringSet(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 将键的整数值按给定的数值增加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long StringIncrement(string key, long value = 1)
        {
            if (!_options.Enabled) return -1;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringIncrement(key, value);
                }
                else
                {
                    return -1;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 给指定键的值减少指定的value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long StringDecrement(string key, long value = 1)
        {
            if (!_options.Enabled) return -1;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringDecrement(key, value);
                }
                else
                {
                    return -1;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 在key键对应值后追加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long StringAppend(string key, string value)
        {
            if (!_options.Enabled) return -1;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).StringAppend(key, value);
                }
                else
                {
                    return -1;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool StringDelete(string key)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).KeyDelete(key);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).KeyExists(key);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 设置键的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan expiry)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).KeyExpire(key, expiry);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 设置键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T value)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
                    return redis.GetDatabase(_options.StorageIndex).StringSet(key, data);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取键的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            if (!_options.Enabled) return default(T);
            if (!KeyExists(key)) return default(T);
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    RedisValue value = redis.GetDatabase(_options.StorageIndex).StringGet(key);
                    if (!value.IsNullOrEmpty)
                    {
                        return JsonConvert.DeserializeObject<T>(value);
                    }
                }
                return default(T);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取指定键的剩余缓存时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TimeSpan? GetExpiry(string key)
        {
            if (!_options.Enabled) return null;
            if (!KeyExists(key)) return null;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    var value = redis.GetDatabase(_options.StorageIndex).StringGetWithExpiry(key);
                    return value.Expiry;
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }
        #endregion


        #region 哈希、散列、字典操作

        /// <summary>
        /// 删除指定的哈希字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string field)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).HashDelete(key, field);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 判断是否存在散列字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool HashKeyExists(string key, string field)
        {
            if (!_options.Enabled) return false;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).HashExists(key, field);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取存储在指定键的哈希字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public object HashGet(string key, string field)
        {
            if (!_options.Enabled) return null;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).HashGet(key, field);
                }
                else
                {
                    return null;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取指定键的哈希中所有字段和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, object> HashGetAll(string key)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!_options.Enabled) return dic;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    var collection = redis.GetDatabase(_options.StorageIndex).HashGetAll(key);
                    foreach (var item in collection)
                    {
                        dic.Add(item.Name, item.Value);
                    }
                }
                return dic;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取哈希中的所有字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] HashKeys(string key)
        {

            if (!_options.Enabled) return new string[] { };
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).HashKeys(key).ToStringArray();
                }
                else
                {
                    return new string[] { };
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 获取散列中的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long HashSize(string key)
        {
            if (!_options.Enabled) return 0;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    return redis.GetDatabase(_options.StorageIndex).HashLength(key);
                }
                else
                {
                    return 0;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }

        /// <summary>
        /// 为多个哈希字段分别设置他们的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        public void HashSet(string key, Dictionary<string, string> dic)
        {
            if (!_options.Enabled) return;
            var redis = GetRedisConnection();
            try
            {
                if (redis != null && redis.IsConnected)
                {
                    List<HashEntry> list = new List<HashEntry>();
                    for (int i = 0; i < dic.Count; i++)
                    {
                        KeyValuePair<string, string> param = dic.ElementAt(i);
                        list.Add(new HashEntry(param.Key, param.Value));
                    }
                    redis.GetDatabase(_options.StorageIndex).HashSet(key, list.ToArray());
                }
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            finally
            {
                _redisPool.EnQueue(redis);
            }
        }
        #endregion



        public void SetDbIndex(int index)
        {
            _options.StorageIndex = index;
        }

        /// <summary>
        /// 启动Redis连接
        /// </summary>
        private void Startup()
        {
            if (_options == null) throw new ArgumentNullException("配置参数不能为空");
            if (_options.Enabled == false) return;

            ConfigurationOptions config = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                ConnectRetry = 10,
                ConnectTimeout = 5000,
                SyncTimeout = 5000,
                EndPoints = { { _options.HostName ?? "localhost" } },
                Password = _options.Password ?? "",
                AllowAdmin = true,
                KeepAlive = 180
            };

            TextWriter log = new LogWriter();

            // 初始化连接池
            _redisPool = new BlockingQueue<ConnectionMultiplexer>(_options.PoolSize);
            for (int i = 0; i < _options.PoolSize; i++)
            {
                var conn = ConnectionMultiplexer.Connect(config, log);
                conn.ConnectionFailed += Conn_ConnectionFailed;
                conn.ErrorMessage += Conn_ErrorMessage;
                conn.ConnectionRestored += Conn_ConnectionRestored;
                _redisPool.EnQueue(conn);
            }
        }

        private void Conn_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _logger.LogInformation($"[Redis连接成功] [{e.EndPoint}] [{e.Exception?.Message ?? ""}]");
        }

        private void Conn_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.LogError("[Redis服务响应失败] [{errMsg}]", e.Message);
        }

        private void Conn_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _logger.LogError("[Redis连接失败] {failedMsg}", e.Exception.Message);
        }

        public void Dispose()
        {
            _redisPool.Clear();
            _logger.LogInformation("[Redis Pool Disposed]");
        }
    }
}
