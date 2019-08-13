using Microliu.Auth.DataMSSQL.Configurations;
using Microliu.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Microliu.Auth.DataMSSQL
{
    public class AuthContext : DbContext
    {
        public virtual DbSet<Role> role { get; set; }

        public static long InstanceCount;

        public AuthContext(DbContextOptions options) : base(options) =>
            Interlocked.Increment(ref InstanceCount);

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    new RoleConfiguration(modelBuilder.Entity<Role>());
        //}
    }
}
