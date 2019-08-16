using Microliu.Auth.DataMySQL;
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
            services.AddTransient<IRoleAppService,RoleAppService>();// 权限服务

            services.AddTransient<IAuthSuperVisor, AuthSupervisor>();// 权限

            services.AddTransient<IRoleRepository, Microliu.Auth.DataMySQL.RoleRepository>();// 角色
            services.AddTransient<IRoleRepository, Microliu.Auth.DataMSSQL.RoleRepository>();// 角色


            //services.AddSingleton<IDbInfo>(new DbInfo(GetConnectionString(configuration))); // 数据库连接
            //services.AddDbContextPool<AuthContext>(options => options.UseSqlServer(connectionString));//Dapper使用

            services.AddDbContextPool<Microliu.Auth.DataMySQL.AuthContextMySQL>(options =>
                        options.UseMySQL(GetConnectionString(configuration, DatabaseType.MySQL)));

            services.AddDbContextPool<Microliu.Auth.DataMSSQL.AuthContext>(options =>
                        options.UseSqlServer(GetConnectionString(configuration, DatabaseType.SQLServer)));
            //services.AddDbContextPool<Microliu.Auth.DataOracle.AuthContext>(options => options.UseOracle(GetConnectionString(configuration, DatabaseType.Oracle)));

            return services;
        }

        public enum DatabaseType
        {
            MySQL = 1,
            SQLServer = 2,
            Oracle = 4
        }
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static string GetConnectionString(IConfiguration configuration, DatabaseType databaseType)
        {
            string connection = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (databaseType == DatabaseType.MySQL)
                {
                    connection = configuration.GetConnectionString("authServiceWindowsMySQL") ?? "";
                }
                else if (databaseType == DatabaseType.Oracle)
                {
                    connection = configuration.GetConnectionString("authServiceWindowsMSSQL") ?? "";
                }
                else if (databaseType == DatabaseType.SQLServer)
                {
                    connection = configuration.GetConnectionString("authServiceWindowsOracle") ?? "";
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("authServiceDocker") ?? "";
            }

            return connection;
        }
    }
}
