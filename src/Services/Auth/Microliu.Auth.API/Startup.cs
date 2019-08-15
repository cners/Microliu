using Microliu.Auth.API.Filters;
using Microliu.Auth.Application;
using Microliu.Core.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Net;

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
                //.AddMvc()
                .AddMvc(c => c.Conventions.Add(new ApiExplorerGroupPerVersionConvention()))
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
                options.SwaggerDoc("v1.0", new Info
                {
                    Title = "API Online Document",
                    Description = "Call API Online Testing",
                    Version = "v1.0",
                    Contact = new Contact
                    {
                        Name = "Liuzhuang",
                        Email = "liuzhuang@6iuu.com"
                    }
                });
                options.SwaggerDoc("v2.0", new Info
                {
                    Title = "API Online Document",
                    Description = "Call API Online Testing",
                    Version = "v2.0",
                    Contact = new Contact
                    {
                        Name = "Liuzhuang",
                        Email = "liuzhuang@6iuu.com"
                    }
                });

                //options.SwaggerDoc("v2.0", new Info { Title = "DFS API -v2", Version = "V2" });

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                options.OperationFilter<SwaggerFileUploadFilter>();
                //options.DocInclusionPredicate((docName, description) => true);
                options.DocInclusionPredicate((version, desc) =>
                {
                    var versions = desc.ControllerAttributes()
                                    .OfType<ApiVersionAttribute>()
                                    .SelectMany(attr => attr.Versions);

                    var maps = desc.ActionAttributes()
                                .OfType<MapToApiVersionAttribute>()
                                .SelectMany(attr => attr.Versions)
                                .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                });
                options.OperationFilter<AddHeaderParameter>();

                // api界面新增authorize按钮，在弹出文本中输入 Bearer +token即可
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Authorization format : Bearer {toekn}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });


                options.IgnoreObsoleteActions();
            });

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

            //app.UseMiddleware(typeof(ExceptionHandlerExtensions));// 异常处理中间件
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    var result = new ReturnResult(context.Response.StatusCode, "权限服务异常，请稍后再试");
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
                });
            });

            // Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "权限服务接口";

                c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "V1.0 Docs");
                c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", "V2.0 Docs");

                //c.DefaultModelExpandDepth(4);
                c.DefaultModelRendering(ModelRendering.Model);
                //c.DefaultModelsExpandDepth(-1);                   // 隐藏展示前端实体
                //c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.EnableValidator();
                c.SupportedSubmitMethods(SubmitMethod.Get,
                                        SubmitMethod.Head,
                                        SubmitMethod.Post,
                                        SubmitMethod.Patch,
                                        SubmitMethod.Delete,
                                        SubmitMethod.Put);
            });
            app.RegisterConsul(lifetime, Configuration);
            app.UseMvc();
        }
    }
}
