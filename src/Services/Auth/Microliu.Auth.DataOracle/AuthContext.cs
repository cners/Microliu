using Microliu.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microliu.Auth.DataOracle
{
    public class AuthContext : DbContext
    {
        public virtual DbSet<Role> role { get; set; }

        public static long InstanceCount;

        public AuthContext(DbContextOptions options) : base(options) =>
            Interlocked.Increment(ref InstanceCount);
    }
}
