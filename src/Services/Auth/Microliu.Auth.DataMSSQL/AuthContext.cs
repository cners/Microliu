using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;

namespace Microliu.Auth.DataMSSQL
{
    public class AuthContext : DbContext
    {
        public DbSet<Role> role { get; set; }
        public DbSet<User> user { get; set; }

        //public DbType DbType { get; set; }

        public static long InstanceCount;

        public AuthContext(DbContextOptions options)
            : base(options)
        {
            Interlocked.Increment(ref InstanceCount);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new RoleConfiguration(modelBuilder.Entity<Role>());
            //modelBuilder.Entity<Role>().Property(b => b.IsEnabled).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=Microliu.Auth;User ID=sa;Password=sa;");
        //}
    }
}
