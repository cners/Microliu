# Microliu.Auth.API （微服务之 - 权限服务）

已经实现了Oralce、SQLserver、Mysql的选用切换，因此为了后面统一数据库，我这里采用了MySQL，故将Oracle、SQLserver的引用类先去掉了。

Microliu.Auth.API的依赖关系是：API -> Application -> (Domain/DataMySQL) -> Infrastructure

API: 接口层，提供HTTP REST接口供其他服务调用
Application: 应用层，提供业务流程整合
Domain：领域层，提供领域模型设计
Data: 数据层，提供灵活的数据支持可满足插拔式切换Oracle、MySQL、SQLserver
Infrastructure: 基础设施层，提供公共的基础服务类



## 恢复数据库(Code First)
### 单库使用的数据库生成方式
*单库，即一个项目使用一个数据库源，数据库表自动生成操作也是相对比较简单*
1. 设置Microliu.Auth.API为默认启动项目
2. 打开工具->NuGet包管理器->Nuget控制台
3. Add-Migration AuthCodeFirst
4. Update-datebase

### 多库的生成方式
*多库，即一个项目使用了多个数据源，比如Microliu.Auth.API支持同时使用Oracle、MySQL、SQLServer，那么生成数据库表的命令就需要特别配置一些参数了*
Add-Migration AuthCodeFirst -ConfigurationTypeName Microliu.Auth.DataMSSQL.AuthContext
update-database -ConfigurationTypeName Microliu.Auth.DataMSSQL.AuthContext


### 可能遇到的问题
提示：No project was found. Change the current working directory or use the --project option.
解决：PM> dotnet ef migrations script --verbose -i --project "E:\Web\Website\Website.MVC" 记得替换--project参数为自己的项目目录

需要更新时，还得使用Remove-Migration移除，然后再重新执行Add-Migration,Update-database


DDD可参考：

[1] https://www.infoq.cn/article/advanced-architecture-aspnet-core/?itm_source=infoq_en&itm_medium=link_on_en_item&itm_campaign=item_in_other_langs
[2] https://www.cnblogs.com/xishuai/p/ddd-repository-iunitofwork-and-idbcontext.html



ABP可参考：
https://www.cnblogs.com/mienreal/tag/ABP/default.html?page=2
