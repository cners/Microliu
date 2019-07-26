using Autofac;
using Microliu.Utils.Database;
using Microliu.Utils.Module;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Microliu.Modules.ORM.DapperIntegration
{
    public class DapperServerModule : ServerModuleBase
    {
        private readonly DapperOptions _options;
        public DapperServerModule(IConfigurationRoot configurationRoot)
            : base(configurationRoot)
        {
            _options = MicroliuAppSettings.GetSection(typeof(DapperOptions).Name).Get<DapperOptions>();
        }

        public override void DoServiceRegister(ContainerBuilder serviceContainerBuilder)
        {
            if (_options != null)
            {
                // register DbConnection
                serviceContainerBuilder.Register((context) =>
                {
                    DbConnection cnn = null;
                    switch (_options.DbType)
                    {
                        case DbType.MySQL:
                            cnn = new MySqlConnection(_options.ConnectionString);
                            break;
                        case DbType.SQLServer:
                            cnn = new SqlConnection(_options.ConnectionString);
                            break;
                        case DbType.Oracle:
                            cnn = new OracleConnection(_options.ConnectionString);
                            break;
                        case DbType.PostgreSQL:
                            cnn = new NpgsqlConnection(_options.ConnectionString);
                            break;
                        case DbType.SQLite:
                            cnn = new SQLiteConnection(_options.ConnectionString);
                            break;
                        default:
                            throw new Exception("please specify DbType!");
                    }
                    return cnn;
                }).As<DbConnection>().InstancePerLifetimeScope();


                // register dbfactory
                DbFactory dbFactory = new DbFactory(_options);
                serviceContainerBuilder.Register((context) =>
                {
                    return dbFactory;
                }).As<IDbFactory>().SingleInstance();

            }
            base.DoServiceRegister(serviceContainerBuilder);
        }
    }
}
