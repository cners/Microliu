using System;

namespace Microliu.Logger
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
