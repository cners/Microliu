using Microliu.Auth.DataMySQL.Interfaces;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Microliu.Auth.DataMySQL
{
    public class AuthDbContext : DbContext, IDbContext
    {
        public virtual DbSet<Role> role { get; set; }
        public virtual DbSet<User> user { get; set; }
        public virtual DbSet<UserPosition> userPosition { get; set; }
        public virtual DbSet<Position> position { get; set; }

        public static long InstanceCount;

        public AuthDbContext(DbContextOptions options)
            : base(options)
        {

            Interlocked.Increment(ref InstanceCount);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server=192.168.30.123;Port=3306;Database=microliu.auth;uid=liuzhuang;pwd=liuzhuang;charset='utf8'");
        //}
    }
}
