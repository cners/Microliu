using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microliu.Auth.Domain;
using Oracle.ManagedDataAccess.Client;

namespace Microliu.Auth.Database
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbInfo _db;
        private readonly IDbConnection _conn;

        public RoleRepository(IDbInfo dbInfo)
        {
            _db = dbInfo;
            //_conn = new OracleConnection(_db.ConnectionString);
            _conn = new SqlConnection(_db.ConnectionString);
        }

        public async Task<Role> AddAsync(Role newEntity, CancellationToken ct = default)
        {
            string sql = "INSERT INTO [Role] ([Id],[RoleName],[CreateTime]) VALUES(@Id, @RoleName, @CreateTime)";
            using (_conn)
            {
                await _conn.ExecuteAsync(sql, newEntity);
            }
            return newEntity;
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Dispose();
            }
        }
    }
}
