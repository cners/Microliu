# Microliu
 
 尝试写的微服务架构，基于.Net core 2.2和Standard 2.0实现。

 目前已经初步实现网关、服务发现、服务自动注册、消息中间件RabbitMQ、熔断、限流、失败重试、身份验证。

 写了几个服务demo：BizLogger（业务日志）、EmailService、SMSService、AuthService（权限管理）、IdentityServer（身份验证）

 还有很多不足之处，正在慢慢完善，不排除已经写过的还存在问题，仅供大家参考，我还会继续学习Surging的架构和其他架构加入进来。

 AuthService本来想写一个完整点的demo，采用DDD架构实现，支持Oracle、SQL Server、MySQL数据库的切换使用。
 
 关于微服务的实现，可参考src目录
 
 关于DDD的实现，可参考src/services/Auth的实现