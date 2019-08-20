using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microliu.Auth.Domain.Entities
{
    [Table("UserPosition")]
    public class UserPosition
    {

        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string PositionId { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public User User { get; set; }

    }
}
