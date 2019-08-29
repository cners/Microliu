using MediatR;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataMySQL
{
    public  class AuthDbContext : DbContext //, IUnitOfWork
    {
        public virtual DbSet<Role> role { get; set; }
        public virtual DbSet<User> user { get; set; }
        public virtual DbSet<UserPosition> userPosition { get; set; }
        public virtual DbSet<Position> position { get; set; }

        private readonly IMediator _mediator;

        public AuthDbContext(DbContextOptions options) : base(options) { }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server=192.168.30.123;Port=3306;Database=microliu.auth;uid=liuzhuang;pwd=liuzhuang;charset='utf8'");
        //}
    }
}
