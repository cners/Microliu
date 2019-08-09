
# 配置自动扫描程序集内需要熔断、限流处理的类
Startup.cs 

`
 public IServiceProvider ConfigureServices(IServiceCollection services)//void 改为 IServiceProvider
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    return services.UseHystrix(Assembly.GetEntryAssembly()); // 新增
}
`

#1. 降级
在函数方法上添加，注意该方法必须是virtual
`
 [HystrixCommand(nameof(HelloFallBackAsync))]
`
上面的 HelloFallBackAsync 是降级执行方法，也支持在降级方法上再加二次降级方法

#2. 熔断
`
 [HystrixCommand(nameof(Hello1FallBackAsync), MaxRetryTimes = 3, EnableCircuitBreater = true)]
`
上面表示最大重试3次，都无效后，执行熔断。默认熔断1000ms，继续执行。