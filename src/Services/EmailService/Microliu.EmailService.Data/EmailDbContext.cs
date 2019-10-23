using Microliu.EmailService.Domain;
using Microliu.EmailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Data
{
    public class EmailDbContext : DbContext
    {

        public EmailDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Project>()
            //    //.HasOne(p => p.User)
            //    .WithMany(u => u.Projects)
            //    .HasForeignKey(p => p.Uid);
        }

        public virtual DbSet<EmailSend> EmailSend { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategory { get; set; }
        public virtual DbSet<RkUserProCategory> RkUserProCategory { get; set; }
        public virtual DbSet<BlackList> BlackList { get; set; }

    }
}
