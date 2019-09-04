using Microliu.EmailService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Domain.Entities
{
    public enum Enabled
    {
        Disabled = 0,
        Enabled = 1
    }

    public enum Deleted
    {
        Deleted = 1,
        NotDelete = 0
    }
    public class BaseEntity : IAggregateRoot
    {
        public virtual Enabled Enabled { get; set; }

        public virtual Deleted Deleted { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
