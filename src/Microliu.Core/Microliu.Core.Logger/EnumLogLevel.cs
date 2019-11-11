using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.Loggers
{
    [Flags]
    public enum LogLevel
    {
        Trace = 1,

        Debug = 2,

        Info = 4,

        Warn = 8,

        Error = 16
    }
}
