关于identityServer4的介绍和基本使用，可以参考CSDN文档：https://blog.csdn.net/qq_42606051/article/details/81583705
或者看原文：https://www.scottbrady91.com/Identity-Server/Getting-Started-with-IdentityServer-4

配置好之后，可访问
http://localhost:10111/.well-known/openid-configuration

**关于UI界面**

你可以在GitHub上获取到UI源代码，并添加到你的IdentityServer4项目中，GitHub地址：https://github.com/IdentityServer/IdentityServer4.Quickstart.UI

简单说一下：
	（1）下载github
	（2）把所有文件Copy到你的IdentityServer4中
	（3）分别添加下面代码Program.cs或Startup.cs中
		 services.AddMvc()
		 
		 app.UseStaticFiles();
         app.UseMvcWithDefaultRoute();

