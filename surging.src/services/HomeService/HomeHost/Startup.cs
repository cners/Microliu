using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Surging.Core.Caching;
using Surging.Core.Caching.Configurations;
using Surging.Core.Codec.MessagePack;
using Surging.Core.Consul;
using Surging.Core.Consul.Configurations;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.DotNetty;
using Surging.Core.EventBusRabbitMQ;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Surging.Core.ProxyGenerator;
using Surging.Core.System.Intercept;

namespace HomeHost
{
    public class Startup
    {
        private ContainerBuilder _builder;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddCacheFile("Configs/cacheSettings.json", optional: false)
                    .AddJsonFile("Configs/appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("Configs/surgingSettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("Configs/consul.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"Configs/appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEventBusFile($"Configs/eventBusSettings.json", optional: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddLogging();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddMicroService(option =>
            {
                option.AddClient();
                option.AddCache();
                option.AddClientIntercepted(typeof(CacheProviderInterceptor));
                //option.UseZooKeeperManager(new ConfigInfo("127.0.0.1:2181"));
                option.UseConsulManager(new ConfigInfo("127.0.0.1:8500"));
                option.UseConsulCacheManager(new ConfigInfo("127.0.0.1:8500"));
                option.UseDotNettyTransport();

                option.UseRabbitMQTransport();
                //option.UseProtoBufferCodec();
                option.UseMessagePackCodec();
                builder.Register(p => new CPlatformContainer(ServiceLocator.Current));
            });

            _builder = builder;
            ServiceLocator.Current = builder.Build();
            return new AutofacServiceProvider(ServiceLocator.Current);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var serviceCacheProvider = app.ApplicationServices.GetRequiredService<ICacheNodeProvider>();
            var addressDescriptors = serviceCacheProvider.GetServiceCaches().ToList();
            app.ApplicationServices.GetRequiredService<IServiceCacheManager>().SetCachesAsync(addressDescriptors);
            loggerFactory.AddConsole();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
