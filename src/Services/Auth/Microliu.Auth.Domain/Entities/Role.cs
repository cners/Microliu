using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microliu.Auth.Domain
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(40), Required]
        public string RoleName { get; set; }
    }
}
