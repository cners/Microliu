using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Microliu.Modules.ORM.DapperIntegration
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        private readonly DapperOptions _options;

        /// <summary>
        /// 构造一个数据工厂
        /// </summary>
        /// <param name="options">数据连接选项</param>
        public DbFactory(DapperOptions options)
        {
            _options = options;
        }


        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public DbConnection Create()
        {
            DbConnection conn = null;
            switch (_options.DbType)
            {
                case DbType.MySQL:
                    conn = new MySqlConnection(_options.ConnectionString);
                    break;
                case DbType.SQLServer:
                    conn = new SqlConnection(_options.ConnectionString);
                    break;
                case DbType.Oracle:
                    conn = new OracleConnection(_options.ConnectionString);
                    break;
                case DbType.PostgreSQL:
                    conn = new NpgsqlConnection(_options.ConnectionString);
                    break;
                case DbType.SQLite:
                    conn = new SQLiteConnection(_options.ConnectionString);
                    break;
                default:
                    throw new Exception("不支持的DbType！");
            }
            return conn;
        }
    }
}
