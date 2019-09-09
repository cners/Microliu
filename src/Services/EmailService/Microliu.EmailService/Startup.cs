using Microliu.Core.Consul;
using Microliu.EmailService.API.Extensions;
using Microliu.EmailService.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


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

            });

            // 添加邮件服务
            services.AddEmailService();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 接口方面
            app.UseErrorHandling();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "emailApi/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "邮件服务接口";
                c.SwaggerEndpoint($"/emailApi/v1.0/swagger.json", "V1.0 Docs");
                c.SwaggerEndpoint($"/emailApi/v2.0/swagger.json", "V2.0 Docs");
                c.RoutePrefix = "apiDoc";
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
            app.UseMicroliuDiscovery();

            // 启用邮件服务
            app.UseEmailService();

            app.UseMvc();
        }
    }
}
