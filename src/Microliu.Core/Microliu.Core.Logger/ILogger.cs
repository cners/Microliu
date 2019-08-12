using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.Logger
{
    public interface ILogger
    {
        void Trace(string content);

        void Debug(string content);

        void Info(string content);

        void Warn(string content);

        void Error(string content);
    }
}
