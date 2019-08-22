using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain
{
    public enum IsEnabled
    {
        Disabled = 0,
        Enabled = 1
    }

    public enum IsDelete
    {
        Deleted = 0,
        NotDelete = 1
    }

    public class BaseEntity
    {
        public virtual IsEnabled IsEnabled { get; set; }

        public virtual IsDelete IsDelete { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }

}
