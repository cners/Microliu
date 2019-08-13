using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microliu.Auth.Domain
{
    [Table("Role")]
    public class Role
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTimeOffset CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        //public string Creator { get; set; }

        /// <summary>
        /// 创建人主键
        /// </summary>
      // public string CreatorId { get; set; }

        /// <summary>
        /// 状态：1正常、-1禁用
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 删除：1-正常、-1删除
        /// </summary>
        public int IsDeleted { get; set; }
    }
}
