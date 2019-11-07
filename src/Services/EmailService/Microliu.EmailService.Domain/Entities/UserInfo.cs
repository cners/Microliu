using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microliu.Utils;

namespace Microliu.EmailService.Domain.Entities
{
    [Table("userinfo")]
    public class UserInfo : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Project> Projects { get; set; }


        public UserInfo()
        {
            this.Enabled = Enabled.Enabled;
            this.Deleted = Deleted.NotDelete;
            this.CreateTime = DateTime.Now;

            Id = SnowflakeId.Default().NextId();
            Email = "";
            Password = "";
        }

        public UserInfo SetUserInfo(string email, string password)
        {
            this.Email = email;
            this.Password = password;
            return this;
        }
    }
}
