using Microliu.Core.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        ////var basePath = PlatformServices.Default.Application.ApplicationBasePath;

        //services.AddIdentityServer()
        //               //.AddDeveloperSigningCredential() //不要在生产环境中使用
        //               //.AddSigningCredential(new X509Certificate2(Path.Combine(basePath,
        //               //Configuration["Certificates:CerPath"], Configuration["Certificates:Password"])))
        //               .AddInMemoryIdentityResources(ConfigIdentity.GetIdentityResources())
        //               .AddInMemoryApiResources(ConfigIdentity.GetApiResources())
        //               .AddInMemoryClients(ConfigIdentity.GetClients())
        //               .AddTestUsers(ConfigIdentity.GetUsers());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
    {
        app.RegisterConsul(lifetime, Configuration);
        app.UseMvc();
    }
}
