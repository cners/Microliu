using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microliu.EmailService.Domain.Entities;
using Microliu.EmailService.Domain.SeedWork;
using Microliu.Utils;

namespace Microliu.EmailService.Domain
{
    [Table("R_USER_PROCATEGORY")]
    public class RkUserProCategory:IAggregateRoot
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("categoryId")]
        public ProjectCategory ProjectCategory { get; set; }

        [ForeignKey("UID")]
        public UserInfo User { get; set; }

        public RkUserProCategory()
        {
            Id = SnowflakeId.Default().NextId();
        }
    }
}
