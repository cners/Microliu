using Microliu.Core.Loggers;
using Microliu.Core.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microliu.Core.RedisCache
{
    public class CacheService : ICacheService
    {
        private RedisOptions _options;
        private ILogger _logger;

        /// <summary>
        /// Redis连接池
        /// </summary>
        private BlockQueue<ConnectionMultiplexer> _redisPool;


        public CacheService(RedisOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public string GetString(string key)
        {
            if (!_options.Startup) return string.Empty;
            var redis = _redisPool.DeQueue(_options.QueueWait);
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

        public void Set(string key, string value, long timeout)
        {
            throw new NotImplementedException();
        }

        public void SetDbIndex(int index)
        {
            throw new NotImplementedException();
        }

        public void Startup()
        {
            if (_options == null) throw new ArgumentNullException("配置参数不能为空");
            if (_options.Startup == false) return;

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
            _redisPool = new BlockQueue<ConnectionMultiplexer>(_options.PoolSize);
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
            _logger.Info($"[{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [Redis重新连接成功] [{e.Exception.Message}]", new string[] { "redis", "connection", "restored" });
        }

        private void Conn_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.Error($"[{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [Redis服务响应失败] [{e.Message}]", new string[] { "redis", "connection", "error" });
        }

        private void Conn_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _logger.Error($"[{DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [Redis连接失败] [{e.Exception.Message}]", new string[] { "redis", "connection", "failed" });
        }

        public void Dispose()
        {
            _redisPool.Clear();
        }
    }
}
