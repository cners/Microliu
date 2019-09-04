using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microliu.EmailService.Domain.Entities
{
    [Table("email_send")]
    public class EmailSend : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string Subject { get; set; }

        [MaxLength]
        public string Body { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        [MaxLength(500)]
        public string CopyTo { get; set; }
    }
}
