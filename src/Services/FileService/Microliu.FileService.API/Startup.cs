using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microliu.Core.Consul;
using Microliu.FileService.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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

            services.AddFileService(Configuration);// 分布式文件服务

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

      
            });
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IApplicationLifetime lifetime)
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

            });
            app.RegisterConsul(lifetime, Configuration);

            var expression = app.UseAutoMapper();
            //expression.CreateMap<User, CreateUserModel>();
            app.UseStateAutoMapper();

            app.UseMvc();
        }
    }
}
