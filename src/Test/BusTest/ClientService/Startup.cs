using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientService.Models;
using ClientService.Services;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ClientService
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
            // IoC - EventBus
            services.AddSingleton(RabbitHutch.CreateBus(Configuration["MQ:Dev"]));
            // IoC - Logger
            //services.AddSingleton<ILogger, ExceptionlessLogger>();
            // IoC - Service & Repository
            services.AddScoped<IClientService, ClientService.Services.ClientService>();
            //services.AddScoped<IClientRepository, ClientRepository>();
            // IoC - DbContext
            //services.AddDbContextPool<ClientDbContext>(
            //    options => options.UseSqlServer(Configuration["DB:Dev"]));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
