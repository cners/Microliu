using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Domain.ViewModels
{
    public class EmailSendDto
    {
        private enum ContentTYPE
        {
            HTML = 0,
            TEXT = 1
        }

        private ContentTYPE ContentType { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string CopyTo { get; set; }

        public string ProjectId { get; set; }

        public EmailSendDto()
        {
            ContentType = ContentTYPE.HTML;
        }
    }
}
