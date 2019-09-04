using Microliu.EmailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Data
{
    public class EmailDbContext : DbContext
    {
        public virtual DbSet<EmailSend> EmailSend { get; set; }

        public EmailDbContext(DbContextOptions options)
            : base(options) { }
    }
}
