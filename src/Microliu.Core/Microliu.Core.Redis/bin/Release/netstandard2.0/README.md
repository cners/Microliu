
### 缓存服务
支持.net core 2.2+，采用Redis缓存，使用阻塞队列创建redis连接池实现。

### 如何使用？
在.net core web项目中使用：

#### 第一种方式：Starup.cs中【推荐】
`
public void ConfigureServices(IServiceCollection services)
{
	...
	services.AddRedis();	//加入Redis
	...
}
`
然后在appsettings.json中加入：
`
{
  "Redis": {
    "HostName": "192.168.10.214:6379",// redis主机和端口地址
    "Password": "",				      // redis密码
    "PoolSize": 20,				 	  // redis连接池大小
    "QueueWait": 400,				  // 等待时间
    "Enabled": true,			 	  // 是否启用redis
    "StorageIndex": 0				  // 存储库空间索引0-15
  }
}
`

#### 第二种方式：Startup.cs中
`
// Redis Cache
services.AddMicroliuRedis(options=>
{
    options.HostName = "192.168.10.214:6379";// redis主机和端口地址
    options.Password = "pt_test";			 // redis密码
    options.PoolSize = 50;					 // redis连接池大小
    options.QueueWait = 600;				 // 等待时间
    options.Enabled = true;					 // 是否启用redis
    options.StorageIndex = 7;				 // 存储库空间索引0-15
});
`


#### 最后

直接在Controller的构造函数中，注入 `ICacheService` 即可。如

```
private ICacheService _cache;
public RedisTestCase(ICacheService cache)
{
    _cache = cache ?? throw new ArgumentNullException(nameof(cache));
}
```