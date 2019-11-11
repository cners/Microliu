using System;
using System.Collections.Generic;
using System.Text;
using Microliu.EmailService.Domain.Entities;

namespace Microliu.EmailService.Application.ViewModel
{
    /// <summary>
    /// 邮箱内容数据传输对象
    /// </summary>
    public class EmailDto
    {
        /// <summary>
        /// 发送标识
        /// </summary>
        public string SendId { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 抄送人
        /// </summary>
        public string CopyTo { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }

        public EmailDto()
        {
            //this.SendId = "";
            //this.Subject = "";
            //this.Body = "";
            //this.Form
            var attributes = typeof(EmailDto).Attributes;
            //foreach (var attribute in attributes.)
            //{
            //    attribute
            //}
            ;
        }


        public static EmailDto ConvertToEmailDto(EmailSend e)
        {
            var ed = new EmailDto();
            ed.SendId = e.Id.ToString();
            ed.SendTime = e.CreateTime == null ? "" : e.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            ed.Subject = e.Subject??"";
            ed.Body = e.Body ?? "";
            ed.From = e.From ?? "";
            ed.To = e.To ?? "";
            ed.CopyTo = e.CopyTo ?? "";
            ed.ProjectId = e.ProjectId.ToString();

            ed.Status = e.Status ?? "";
            ed.ErrorMessage = e.ErrorMessage ?? "";
            return ed;
        }

    }
}
