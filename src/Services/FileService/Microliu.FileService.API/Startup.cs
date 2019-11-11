using Microliu.Core.Consul;
using Microliu.FileService.API.Configurations;
using Microliu.FileService.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Microliu.FileService.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // 文件上传大小限制
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
            });

            services.AddFileService(Configuration);// 分布式文件服务

            services.AddAppSettings(Configuration);

            //services.AddMvcCore().AddApiExplorer();

            // 版本控制
            //services.AddApiVersioning(options =>
            //{
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //});
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = false;
            });
            services.AddMvcCore();
            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Swagger


            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"API Online Document (v{description.ApiVersion})",
                        Version = description.ApiVersion.ToString(),
                        Description = "切换版本请点右上角版本切换",
                        Contact = new OpenApiContact { Name = "刘壮", Email = "liuzhuang@6iuu.com" }
                    });
                }

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);


            });
            services.AddAutoMapper();
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
                c.DocumentTitle = "文件服务接口";
                //foreach (var description in c)
                {
                    c.SwaggerEndpoint($"/swagger/v{1}/swagger.json", "v1");
                    c.SwaggerEndpoint($"/swagger/v{2}/swagger.json", "v2");
                }
                //c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "HTTP接口");
                c.RoutePrefix = "apiDoc";

            });
            app.UseMicroliuDiscovery();

            var expression = app.UseAutoMapper();
            //expression.CreateMap<User, CreateUserModel>();
            app.UseStateAutoMapper();
            app.UseMvc();
        }
    }
}
