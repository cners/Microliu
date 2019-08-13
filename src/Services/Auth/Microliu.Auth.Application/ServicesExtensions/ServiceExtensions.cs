using Microliu.Auth.DataMSSQL;
using Microliu.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace Microliu.Auth.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthAppService, AuthAppService>();// 权限服务

            services.AddTransient<IAuthSuperVisor, AuthSupervisor>();// 权限

            services.AddTransient<IRoleRepository, RoleRepository>();// 角色


            var connectionString = GetConnectionString(configuration);
            //services.AddSingleton<IDbInfo>(new DbInfo(GetConnectionString(configuration))); // 数据库连接
            services.AddDbContextPool<AuthContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static string GetConnectionString(IConfiguration configuration)
        {
            string connection = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = configuration.GetConnectionString("authServiceWindows") ??
                    "Data Source=192.168.10.206/qiche;User ID=qiche_test;Password=test#.qc2018";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("authServiceDocker") ??
                    "Data Source=192.168.10.206/qiche;User ID=qiche_test;Password=test#.qc2018";
            }
            return connection;
        }
    }
}
