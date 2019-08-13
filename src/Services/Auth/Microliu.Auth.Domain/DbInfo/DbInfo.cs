namespace Microliu.Auth.Domain
{
    public class DbInfo : IDbInfo
    {
        public string ConnectionString { get; }

        public DbInfo(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
