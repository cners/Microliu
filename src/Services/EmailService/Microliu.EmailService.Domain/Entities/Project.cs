using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microliu.EmailService.Domain.Entities;
using Microliu.Utils;

namespace Microliu.EmailService.Domain
{
    [Table("project")]
    public class Project : BaseEntity
    {
        [Key]

        public long Id { get; set; }

        public string Name { get; set; }


        [ForeignKey("UID")]
        public virtual UserInfo User { get; set; }

        [ForeignKey("categoryId")]
        public virtual ProjectCategory Category { get; set; }

        public Project()
        {
            Id = SnowflakeId.Default().NextId();
            Name = "";
            User = new UserInfo();
        }

        public Project SetCreateInfo(string name)
        {
            this.Name = name;
            return this;
        }
    }
}
