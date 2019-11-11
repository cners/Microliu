
# 缓存服务
基于DotNetCore 2.2实现，文本日志采用NLog，为了更好的支持webapi分布式日志记录，也同时支持了Exceptionless

## 如何使用？
在AspNetCore中使用：

`
// 日志服务
services.AddTransient<ILogger, Logger>();

// Redis Cache
services.AddMicroliuRedis(options=>
{
    options.HostName = "192.168.10.214:6379"; // redis主机和端口地址
    options.Password = "pt_test";// redis密码
    options.PoolSize = 50;		// redis连接池大小
    options.QueueWait = 600;	// 等待时间
    options.Startup = true;		// 是否启用redis
    options.StorageIndex = 7;	// 存储库空间索引
});
`

## 特别注意
如果在webapi中希望使用Exceptionless日志的话，请添加

app.UseExceptionless(Configuration);

另外需要在appSettings.json中添加

`
{
   ....
  "Exceptionless": {
    "ApiKey": "ov7WSaIWUYaeIRnenHCrxkmBt9OSPZHAFBvyHopY",
    "ServerUrl": "http://192.168.10.214:11012"
  }
  ...
}
`

另外，请再加入一个NLog.config文件，并在运行目录添加文件夹logs，否则是不会自动记录到文本文档中的。