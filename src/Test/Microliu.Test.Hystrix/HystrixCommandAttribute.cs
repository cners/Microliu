using AspectCore.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using System;
using System.Threading.Tasks;

namespace Microliu.Test.Hystrix
{
    /// <summary>
    /// 熔断框架
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HystrixCommandAttribute : AbstractInterceptorAttribute
    {
        #region 属性
        /// <summary>
        /// 最多重试次数，如果为0则不重试
        /// </summary>
        public int MaxRetryTimes { get; set; } = 0;

        /// <summary>
        /// 重试间隔的毫秒数
        /// </summary>
        public int RetryIntervalMilliseconds { get; set; } = 100;

        /// <summary>
        /// 是否启用熔断
        /// </summary>
        public bool EnableCircuitBreater { get; set; } = false;

        /// <summary>
        /// 熔断前允许出现错误次数
        /// </summary>
        public int ExceptionAllowedBeforeBreaking { get; set; } = 3;

        /// <summary>
        /// 熔断多长时间（毫秒）
        /// </summary>
        public int MillisecondOfBreak { get; set; } = 1000;

        /// <summary>
        /// 执行超过多少毫秒认为超时（0表示不检测超时）
        /// </summary>
        public int TimeoutMilliseconds { get; set; } = 0;

        /// <summary>
        /// 缓存多少毫秒（0表示不缓存），用“类名+方法名+所有参数ToString拼接”做缓存key
        /// </summary>
        public int CacheTTLMilliseconds { get; set; } = 0;

        private IAsyncPolicy policy;

        // 缓存
        private static readonly IMemoryCache memoryCache = new MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions());

        /// <summary>
        /// 降级方法
        /// </summary>
        public string FallBackMethod { get; set; }
        #endregion

        /// <summary>
        /// 熔断框架
        /// </summary>
        /// <param name="fallBackMethod">降级方法</param>
        public HystrixCommandAttribute(string fallBackMethod)
        {
            this.FallBackMethod = fallBackMethod;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            lock (this)
            {
                if (policy == null)
                {
                    policy = Policy.NoOpAsync();
                    if (MaxRetryTimes > 0)// 重试
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().WaitAndRetryAsync(MaxRetryTimes, i => TimeSpan.FromMilliseconds(RetryIntervalMilliseconds)));
                    }

                    if (EnableCircuitBreater)// 熔断
                    {
                        policy = policy.WrapAsync(Policy.Handle<Exception>().CircuitBreakerAsync(ExceptionAllowedBeforeBreaking, TimeSpan.FromMilliseconds(MillisecondOfBreak)));
                    }

                    if (TimeoutMilliseconds > 0)// 超时
                    {
                        policy = policy.WrapAsync(Policy.TimeoutAsync(() => TimeSpan.FromMilliseconds(TimeoutMilliseconds), Polly.Timeout.TimeoutStrategy.Pessimistic));
                    }

                    var policyFallback = Policy.Handle<Exception>()
                    .FallbackAsync(async (ctx, t) =>
                    {
                        AspectContext aspectContext = (AspectContext)ctx["aspectContext"];
                        var fallBackMethod = context.ServiceMethod.DeclaringType.GetMethod(this.FallBackMethod);
                        Object fallBackResult = fallBackMethod.Invoke(context.Implementation, context.Parameters);
                        //不能如下这样，因为这是闭包相关，如果这样写第二次调用Invoke的时候context指向的
                        //还是第一次的对象，所以要通过Polly的上下文来传递AspectContext
                        //context.ReturnValue = fallBackResult;
                        aspectContext.ReturnValue = fallBackResult;
                    }, async (ex, t) => { });
                    policy = policyFallback.WrapAsync(policy);

                }
            }

            // 本地调用的AspectContext传递给Polly,主要给FallBackMethod中使用，避免闭包的坑
            Context pollyCtx = new Context();
            pollyCtx["aspectContext"] = context;

            if (CacheTTLMilliseconds > 0)
            {
                // 用类名+方法名+参数的下划线连接起来作为缓存key
                string cacheKey = "HystrixMethodCacheManager_Key_" + context.ServiceMethod.DeclaringType + "." + context.ServiceMethod + string.Join("_", context.Parameters);

                // 尝试从缓存中获取
                if (memoryCache.TryGetValue(cacheKey, out var cacheValue))
                {
                    context.ReturnValue = cacheValue;
                }
                else
                {
                    await policy.ExecuteAsync(ctx => next(context), pollyCtx);
                    using (var cacheEntry = memoryCache.CreateEntry(cacheKey))
                    {
                        cacheEntry.Value = context.ReturnValue;
                        cacheEntry.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMilliseconds(CacheTTLMilliseconds);
                    }
                }
            }
            else
            {
                await policy.ExecuteAsync(ctx => next(context), pollyCtx);
            }
        }
    }
}
