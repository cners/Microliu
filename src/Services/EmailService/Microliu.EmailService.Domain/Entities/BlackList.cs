using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microliu.EmailService.Domain.SeedWork;
using Microliu.Utils;

namespace Microliu.EmailService.Domain
{
    [Table("blacklist")]
    public class BlackList : IAggregateRoot
    {
        [Key]
        public long Id { get; set; }

        public string Email { get; set; }

        public DateTime? CreateTime { get; set; }

        public BlackList()
        {
            Id = SnowflakeId.Default().NextId();
            CreateTime = DateTime.Now;
        }
    }
}
