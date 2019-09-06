using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microliu.Auth.DataMySQL
{
    public  class AuthDbContext : DbContext
    {
        public virtual DbSet<Role> role { get; set; }
        public virtual DbSet<User> user { get; set; }
        public virtual DbSet<UserPosition> userPosition { get; set; }
        public virtual DbSet<Position> position { get; set; }

        public AuthDbContext(DbContextOptions options) : base(options) { }
    }
}
