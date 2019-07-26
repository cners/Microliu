using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Microliu.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

                    services.AddIdentityServer()
                            .AddDeveloperSigningCredential() //不要在生产环境中使用
                            .AddInMemoryIdentityResources(ConfigIdentity.GetIdentityResources())
                            .AddInMemoryApiResources(ConfigIdentity.GetApiResources())
                            .AddInMemoryClients(ConfigIdentity.GetClients())
                            .AddTestUsers(ConfigIdentity.GetUsers());
                })
                .Configure(app =>
                {
                    app.UseIdentityServer();

                    app.UseStaticFiles();
                    app.UseMvcWithDefaultRoute();
                });
    }
}
