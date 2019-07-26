using System;

namespace Microliu.Modules.ORM.DapperIntegration
{
    /// <summary>
    /// Dapper连接选项
    /// </summary>
    public class DapperOptions
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType { get; set; }
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    [Flags]
    public enum DbType
    {
        MySQL = 1,
        SQLServer = 2,
        Oracle = 4,
        PostgreSQL = 8,
        SQLite = 16
    }
}
