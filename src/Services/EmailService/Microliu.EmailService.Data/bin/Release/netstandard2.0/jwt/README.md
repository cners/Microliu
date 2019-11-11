
使用方法：application.json 加入
  "Jwt": {
    "Enabled": false,
    "Issuer": "issuer", //随意定义
    "Audience": "Audience", //随意定义
    "SecretKey": "abc", //随意定义
    "Lifetime": 20, //单位分钟
    "ValidateLifetime": true, //验证过期时间
    "HeadField": "Authorization", //头字段
    "Prefix": "Bearer", //前缀
    "IgnoreUrls": [ "/papi/v1.0/partner/login", "/papi/v1.0/partner/loginvcode/*" ] //忽略验证的url(不区分大小写)
  }

	//Jwt注入
    services.AddTransient<IJwt, Jwt>();


    public static IApplicationBuilder UsePartnerService(this IApplicationBuilder app)
    {
        app.UseJwt();      
        return app;
    }