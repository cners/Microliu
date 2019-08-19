using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microliu.Auth.Domain.Entities
{
    [Table("AuthUser")]
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required, MaxLength(20),DisplayName("用户名")]
        public string UserName { get; set; }

        [Required,MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(40),StringLength(100)]
        public string NickName { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
