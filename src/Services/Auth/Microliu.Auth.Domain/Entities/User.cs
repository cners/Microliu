using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Microliu.Auth.Domain.Entities
{
    [Table("AuthUser")]
    public class User : BaseEntity
    {
        [Key]
        public string Id { get; set; }

        [Required, MaxLength(20), DisplayName("用户名")]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(40), StringLength(100)]
        public string NickName { get; set; }

        [MaxLength(40)]
        public string Email { get; set; }

        public virtual IQueryable<UserPosition> UserPositions { get; set; }
    }
}
