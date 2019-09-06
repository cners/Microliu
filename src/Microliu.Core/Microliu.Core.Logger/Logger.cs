using Exceptionless;
using System;

namespace Microliu.Core.Logger
{
    public class Logger : ILogger
    {
        NLog.Logger _nLogger = NLog.LogManager.GetCurrentClassLogger();
        private ExceptionlessClient _exless = ExceptionlessClient.Default;

        public void Debug(string content)
        {
            _exless.CreateLog(this.GetType().FullName, content).Submit();
            _nLogger.Debug(content);
        }

        public void Error(string content)
        {
            _nLogger.Error(content);
        }

        public void Info(string content)
        {
            _nLogger.Info(content);
        }

        public void Trace(string content)
        {
            _nLogger.Trace(content);
        }

        public void Warn(string content)
        {
            _nLogger.Warn(content);
        }

    }
}
