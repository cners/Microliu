using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.Doamin
{
    public class NewsInfo : FullAuditedEntity<string>
    {
        public NewsInfo()
        {
            Id = Guid.NewGuid().ToString();
            Title = string.Empty;
            Content = string.Empty;
            ViewCount = 0;
            CreateTime = DateTime.Now;
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public long ViewCount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
