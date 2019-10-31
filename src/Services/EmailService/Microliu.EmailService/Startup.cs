using Exceptionless;
using Microliu.Core.Consul;
using Microliu.EmailService.API.Extensions;
using Microliu.EmailService.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microliu.EmailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => {
                // 添加路由前缀
                //opt.UseCentralRoutePrefix(new RouteAttribute("emailapi"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // Swagger 接口文档
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "API Online Document",
                    Version = "v1.0",
                    Contact = new OpenApiContact { Name = "Liu Zhuang", Email = "liuzhuang@6iuu.com" }
                });
                options.SwaggerDoc("v2.0", new OpenApiInfo
                {
                    Title = "API Online Document",
                    Version = "v2.0",
                    Contact = new OpenApiContact { Name = "Liu Zhuang", Email = "liuzhuang@6iuu.com" }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);


                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    //if (apiDesc.RelativePath == "papi/v{version}/Partner/Login")
                    //    ;
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var apiVersions = methodInfo.DeclaringType
                                             .GetCustomAttributes(true)
                                             .OfType<ApiVersionAttribute>()
                                             .SelectMany(attr => attr.Versions);

                    var mapToApiVersions = methodInfo.DeclaringType
                                        .GetCustomAttributes(true)
                                        .OfType<MapToApiVersionAttribute>()
                                        .SelectMany(attr => attr.Versions)
                                        .ToArray();

                    //return versions.Any(v => $"v{v.ToString()}" == docName);
                    return apiVersions.Any(v => $"v{v.ToString()}" == docName) && (mapToApiVersions.Length == 0 || mapToApiVersions.Any(v => $"v{v.ToString()}" == docName));

                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Authorization format : Bearer {toekn}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                //c.TagActionsBy(api => api.HttpMethod);// 通过HttpMethod进行展示分类接口
                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

            });

            // 添加邮件服务
            services.AddEmailService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 接口方面
            app.UseExceptionless(Configuration);
            app.UseRequestResponseLogging();
            app.UseErrorHandling();
            app.UseStaticFiles();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "邮件服务接口";
                c.RoutePrefix = "docs";


                c.SwaggerEndpoint($"/v1.0/swagger.json", "V1.0 Docs");
                c.SwaggerEndpoint($"/v2.0/swagger.json", "V2.0 Docs");
                //c.SwaggerEndpoint($"/emailApi/v1.0/swagger.json", "V1.0 Docs");
                //c.SwaggerEndpoint($"/emailApi/v2.0/swagger.json", "V2.0 Docs");
                //c.RoutePrefix = "emailApi";
                c.DefaultModelExpandDepth(4);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.EnableValidator();
            });

            // 微服务服务发现
            //app.UseMicroliuDiscovery();

            // 启用邮件服务
            app.UseEmailService();

            app.UseMvc();
        }
    }
}
