

### 新建Asp.net core 2.2+项目

### 【注意】该类已暂停使用，已转用ELK

### 安装NuGet

	Exceptionless 4.3.2027
	Exceptionless.AspNetCore 4.3.2027
	Exceptionless.NLog 4.3.2027
	NLog 4.6.8
	NLog.Web.AspNetCore 4.9.0

### 新增配置文件nlog.config

	<?xml version="1.0" encoding="utf-8" ?>
	<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
		  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
		  autoReload="true"
		  internalLogLevel="Info"
		  internalLogFile="c:\temp\internal-nlog.txt">

	  <!-- enable asp.net core layout renderers -->
	  <extensions>
		<add assembly="NLog.Web.AspNetCore"/>
		<add assembly="Exceptionless.NLog"/>
	  </extensions>

	  <!-- the targets to write to -->
	  <targets>
		<!-- write logs to file  -->
		<target xsi:type="File" name="allfile" fileName="${basedir}/logs/${shortdate}(all).log"
				layout="${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}" />

		<!-- another file log, only own logs. Uses some ASP.NET core renderers -->
		<target xsi:type="File" name="ownFile-web" fileName="${basedir}/logs/${shortdate}(own).log"
				layout="${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}|url: ${aspnet-request-url}|action: ${aspnet-mvc-action}" />

		<target name="logconsole" xsi:type="Console" />


		<target xsi:type="Exceptionless" name="exceptionless" apiKey="ov7WSaIWUYaeIRnenHCrxkmBt9OSPZHAFBvyHopY" serverUrl="http://10.0.0.101:11012">
           		<field name="host" layout="${machinename}" />
           		<field name="identity" layout="${identity}" />
           		<field name="windows-identity" layout="${windows-identity:userName=True:domain=False}" />
           		<field name="process" layout="${processname}" />
         
		</target>
	  </targets>

	  <!-- rules to map from logger name to target -->
	  <rules>
		<!--All logs, including from Microsoft-->
		<logger name="*" minlevel="Trace" writeTo="allfile" />

		<!--Skip non-critical Microsoft logs and so log only own logs-->
		<logger name="Microsoft.*" maxlevel="Info" final="true" />
		<!-- BlackHole without writeTo -->
		<logger name="*" minlevel="Trace" writeTo="ownFile-web" />
		<logger name="*" minlevel="Trace" writeTo="logconsole" />
		<logger name="*" minlevel="Trace" writeTo="exceptionless" />
	  </rules>
	</nlog>

application.*.json
	{
	  "Logging": {
		"IncludeScopes": true,
		"LogLevel": {
		  "Default": "Information", //Trace,Information,Warning,Error,Debug
		  "Microsoft": "Warning",
		  "Microsoft.Hosting.Lifetime": "Information"
		}
	  }
	}

### 写入代码到Program.cs

	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using NLog.Web;
	using System;

	namespace Microliu.Core.LoggerTest
	{
		public class Program
		{
			public static void Main(string[] args)
			{
				var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
				try
				{
					logger.Debug("[启动程序] [Main]");
					CreateHostBuilder(args).Build().Run();
				}
				catch (Exception exception)
				{
					//NLog: catch setup errors
					logger.Error(exception, "[程序执行中异常]");
					throw;
				}
				finally
				{
					// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
					NLog.LogManager.Shutdown();
					logger.Info("[程序停止运行]");
				}
			}

			public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
					.ConfigureWebHostDefaults(webBuilder =>
					{
						webBuilder.UseStartup<Startup>();
					}).ConfigureLogging(logging =>
					{
						logging.ClearProviders();
						logging.SetMinimumLevel(LogLevel.Trace);
					})
					.UseNLog();// NLog: Setup NLog for Dependency injection
		}
	}


### 修改配置文件

连接Exceptionless
修改nlog.config中的 apiKey和serverUrl即可
<target xsi:type="Exceptionless" name="exceptionless" apiKey="apiKey" serverUrl="http://10.0.0.101:11012">



