﻿using Microliu.Auth.DataMySQL;
using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Repositories;
using Microliu.Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microliu.Auth.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthApplication, AuthApplication>();// 权限服务

            //services.AddTransient<IAuthSuperVisor, AuthSupervisor>();// 权限

            services.AddTransient<IRoleRepository, RoleRepository>();// 角色
            services.AddTransient<IUserRepository, UserRepository>();// 员工
            services.AddTransient<IPositionRepository, PositionRepository>();// 岗位

            services.AddDbContextPool<AuthDbContext>(options =>
                        options.UseMySQL(GetConnectionString(configuration, DatabaseType.SQLServer)));

            services.AddTransient<IDbContext, AuthDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services.AddDbContextPool<AuthDbContext>(options =>
            //            options.UseSqlServer(GetConnectionString(configuration, DatabaseType.SQLServer)));


            //services.AddDbContextPool<Microliu.Auth.DataOracle.AuthContext>(options => options.UseOracle(GetConnectionString(configuration, DatabaseType.Oracle)));

            return services;
        }

        private enum DatabaseType
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

        /// <summary>
        /// 扩展满足获取不同数据库源的服务接口
        /// 需要实现GetDbType方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static T GetServices<T>(this IServiceProvider provider, DbType dbType)
        {
            var services = provider.GetServices<T>();
            return services.Where(r => (DbType)r.GetType().GetMethod("GetDbType").Invoke(r, null) == dbType).FirstOrDefault();
        }
    }
}
