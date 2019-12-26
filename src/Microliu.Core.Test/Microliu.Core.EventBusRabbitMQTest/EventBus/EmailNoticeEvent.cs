using Microliu.Core.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.Core.EventBusTest.EventBus
{
    public class EmailNoticeEvent : IntegrationEvent
    {
        public Guid NoticeId { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public static EmailNoticeEvent Default()
        {
            var emailNotice = new EmailNoticeEvent();
            emailNotice.NoticeId = Guid.NewGuid();
            emailNotice.From = "liuzhuang@lzassist.com";
            emailNotice.To = "xiaozbc@sina.com";
            emailNotice.Title = "测试发布标题";
            emailNotice.Content = "测试发布内容";
            return emailNotice;
        }
    }
}
