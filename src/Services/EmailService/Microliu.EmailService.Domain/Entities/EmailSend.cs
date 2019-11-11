using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microliu.EmailService.Domain.Entities
{
    /// <summary>
    /// Email Send Log
    /// </summary>
    [Table("email_send_log")]
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


        /// <summary>
        /// 状态：success,fail,sending
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 所属项目主键
        /// </summary>
        public long ProjectId { get; set; }


        public EmailSend SetSendError(string errorMessage)
        {
            this.Status = "fail";
            this.ErrorMessage = (errorMessage ?? "").Length > 2000 ? errorMessage.Substring(0, 2000) : errorMessage;
            return this;
        }
    }
}
