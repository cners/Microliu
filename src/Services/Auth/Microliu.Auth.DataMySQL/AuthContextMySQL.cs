using Microliu.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Microliu.Auth.DataMySQL
{
    public class AuthContextMySQL : DbContext
    {
        public virtual DbSet<Role> role { get; set; }

        public static long InstanceCount;

        public AuthContextMySQL(DbContextOptions options)
            : base(options)
        {

            Interlocked.Increment(ref InstanceCount);
        }
    }
}
