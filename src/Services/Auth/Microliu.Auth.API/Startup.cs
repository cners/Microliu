using Microliu.Auth.API.Extensions;
using Microliu.Auth.API.Filters;
using Microliu.Auth.Application;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using Microliu.Core.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Microliu.Auth.API
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
            services
                .AddMvc()
                //.AddMvc(c => c.Conventions.Add(new ApiExplorerGroupPerVersionConvention()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthService(Configuration);// 权限服务

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Swagger


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

                //options.OperationFilter<RemoveVersionFromParameter>();
                //options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                //options.OperationFilter<SwaggerFileUploadFilter>();
                //options.DocInclusionPredicate((docName, description) => true);
                //options.DocInclusionPredicate((version, desc) =>
                //{
                //    var versions = desc.ControllerAttributes()
                //                    .OfType<ApiVersionAttribute>()
                //                    .SelectMany(attr => attr.Versions);

                //    var maps = desc.ActionAttributes()
                //                .OfType<MapToApiVersionAttribute>()
                //                .SelectMany(attr => attr.Versions)
                //                .ToArray();

                //    return versions.Any(v => $"v{v.ToString()}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                //});
                //options.OperationFilter<AddHeaderParameter>();

                // api界面新增authorize按钮，在弹出文本中输入 Bearer +token即可
                //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "Authorization format : Bearer {toekn}",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});


                //options.IgnoreObsoleteActions();
            });
            services.AddAutoMapper();
            //services.ConfigureSwaggerGen(c =>
            //{
            //    // 配置生成的 xml 注释文档路径
            //    var rootPath = AppContext.BaseDirectory;
            //    c.IncludeXmlComments(Path.Combine(rootPath, "Microliu.Auth.API.xml"));
            //    //c.IncludeXmlComments(Path.Combine(rootPath, "AuthApi.xml"));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));// 异常处理中间件

            // Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "权限服务接口";
                c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "V1.0 Docs");
                c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", "V2.0 Docs");
                c.RoutePrefix = "apiDoc";

                //c.DefaultModelExpandDepth(4);
                //c.DefaultModelRendering(ModelRendering.Model);
                ////c.DefaultModelsExpandDepth(-1);                   // 隐藏展示前端实体
                ////c.DisplayOperationId();
                //c.DisplayRequestDuration();
                //c.DocExpansion(DocExpansion.None);
                //c.EnableDeepLinking();
                //c.EnableFilter();
                //c.MaxDisplayedTags(5);
                //c.ShowExtensions();
                //c.EnableValidator();
                //c.SupportedSubmitMethods(SubmitMethod.Get,
                //                        SubmitMethod.Head,
                //                        SubmitMethod.Post,
                //                        SubmitMethod.Patch,
                //                        SubmitMethod.Delete,
                //                        SubmitMethod.Put);
            });
            app.RegisterConsul(lifetime, Configuration);

            var expression = app.UseAutoMapper();
            expression.CreateMap<User, CreateUserModel>();
            app.UseStateAutoMapper();
            app.UseMvc();
        }
    }

}
