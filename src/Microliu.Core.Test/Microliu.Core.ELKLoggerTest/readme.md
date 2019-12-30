
### Microliu.Core.ELKLoggerTest

简介：采用NLog实现向ELK中插入日志数据，通过访问Kibana实现对日志的检索。ELK是部署在CentOS7.6服务器上的docker中，具体部署过程在此处不作描述。

### ELK
ElasticSearch 7.5.1
Logstash	  7.5.1
Kibana		  7.5.1


### 使用方法
curl -X GET /LogTest/Add?content=[log content]
curl -X GET /LogTest/AddLogs?total=[log total]&tag=[log tag]

示例：
curl -X GET /LogTest/Add?content=传入参数xxx
curl -X GET /LogTest/AddLogs?total=10000&tag=测试一万条日志插入

### 安装步骤
Nuget:
NLog.Targets.ElasticSearch 7.1.0
NLog.Web.AspNetCore 4.9.0

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
    .UseNLog();  《---------新增

添加nlog.config
'''
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      internalLogToConsole="true">
  <extensions>
    <add assembly="NLog.Targets.ElasticSearch"/>
  </extensions>

  <targets async="true">
    <!--https://github.com/reactive-markets/NLog.Targets.ElasticSearch/wiki-->
    <target xsi:type="ElasticSearch"
            name="ElasticSearch"
            uri="http://39.107.24.71:9040"
            index="Web"
            documentType="logevent"
            includeAllProperties="false"
            requireAuth="false">
      <field name="host" layout="${machinename}" />
      <field name="application"
                 layout="${applicationName}" />
      <field name="logged" layout="${date}" />
      <field name="level" layout="${level}" />
      <field name="message" layout="${message}" />
      <field name="logger" layout="${logger}" />
      <field name="callSite" layout="${callsite:filename=true}" />
      <field name="exception" layout="${exception:tostring}" />
      <field name="IP" layout="${aspnet-request-ip}" />
      <field name="User" layout="${aspnetcore-request-user}" />
      <field name="serverName" layout="${machinename}" />
      <field name="url" layout="${aspnetcore-request-url}" />
    </target>

    <target name="logconsole" xsi:type="Console" />

  </targets>
  <rules>
    <logger name="*" minlevel="INFO" writeTo="ElasticSearch" />
    <logger name="*" minlevel="Trace" writeTo="logconsole" />
  </rules>
</nlog>
'''

在控制器声明直接使用 Microsoft.Extensions.Logging.ILogger 后，使用_logger.LogInformation($"[ELK日志测试]");