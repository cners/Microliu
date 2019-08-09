
# Microliu.Core.Consul
  
【服务自动注册到Consul】
1. 新建AspNetCore WebApi程序，支持asp.net core 2.2
2. 在Startup.cs中新增代码
`
public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
   
   ...

	app.RegisterConsul(lifetime, Configuration);   // 增加服务注册
	
	...
	app.UseMvc(routes =>
	{
		routes.MapRoute(
			name: "default",
			template: "{controller=Home}/{action=Index}/{id?}");
	});
}
`
3. appsettings.json
新增ConsulOptions节点
`
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConsulOptions": {
    "Ip": "localhost",
    "Port": 8500,
    "ServiceGroups": "",
    "ServiceName": "EmailService", // 服务名称
    "Tags": [ "EmailService", "邮件服务" ], // 服务标记
    "HealthCheckPath": "/api/health",//健康检查路径

    "LocalhostIp": "localhost", // 本地webapi ip地址
    "LocalhostPort": 9401 // 本地端口
  }
}
`