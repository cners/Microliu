using Exceptionless;
using System;

namespace Microliu.Core.Loggers
{
    public class Logger : ILogger
    {
        NLog.Logger _nLogger = NLog.LogManager.GetCurrentClassLogger();
        private ExceptionlessClient _exless = ExceptionlessClient.Default;

        public ExceptionlessClient Exless
        {
            get { return _exless; }
        }

        public void Debug(string content)
        {
            _exless.SubmitLog(content);
            _nLogger.Debug(content);
        }


        public void Debug(string content, params string[] tags)
        {
            _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Debug).AddTags(tags).Submit();
            _nLogger.Debug(content);
        }
        public void Debug(string content, string[] tags, params object[] datas)
        {
            var eventBuilder = _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Debug).AddTags(tags);
            foreach (var data in datas)
            {
                eventBuilder.AddObject(data);
            }
            eventBuilder.Submit();
            _nLogger.Debug(content);
        }

        public EventBuilder DebugBuilder(string content)
        {
            return _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Debug);
        }

        public void Error(string content)
        {
            _exless.SubmitLog(content, Exceptionless.Logging.LogLevel.Error);
            _nLogger.Error(content);
        }

        public void Error(string content, params string[] tags)
        {
            _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Error).AddTags(tags).Submit();
            _nLogger.Error(content);
        }

        public void Error(string content, string[] tags, params object[] datas)
        {
            var eventBuilder = _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Error).AddTags(tags);
            foreach (var data in datas)
            {
                eventBuilder.AddObject(data);
            }
            eventBuilder.Submit();
            _nLogger.Error(content);
        }

        public EventBuilder ErrorBuilder(string content)
        {
            return _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Error);
        }

        public void Info(string content)
        {
            _exless.SubmitLog(content, Exceptionless.Logging.LogLevel.Info);
            _nLogger.Info(content);
        }

        public void Info(string content, params string[] tags)
        {
            _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Info).AddTags(tags).Submit();
            _nLogger.Info(content);
        }

        public void Info(string content, string[] tags, params object[] datas)
        {
            var eventBuilder = _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Info).AddTags(tags);
            foreach (var data in datas)
            {
                eventBuilder.AddObject(data);
            }
            eventBuilder.Submit();
            _nLogger.Info(content);
        }

        public EventBuilder InfoBuilder(string content)
        {
            return _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Info);
        }

        public EventBuilder ToException(Exception exception)
        {
            return _exless.CreateException(exception);
        }

        public void Trace(string content)
        {
            _exless.SubmitLog(content, Exceptionless.Logging.LogLevel.Trace);
            _nLogger.Trace(content);
        }

        public void Trace(string content, params string[] tags)
        {
            _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Trace).AddTags(tags).Submit();
            _nLogger.Trace(content);
        }

        public void Trace(string content, string[] tags, params object[] datas)
        {
            var eventBuilder = _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Trace).AddTags(tags);
            foreach (var data in datas)
            {
                eventBuilder.AddObject(data);
            }
            eventBuilder.Submit();
            _nLogger.Trace(content);
        }

        public EventBuilder TraceBuilder(string content)
        {
            return _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Trace);
        }

        public void Warn(string content)
        {
            _exless.SubmitLog(content, Exceptionless.Logging.LogLevel.Warn);
            _nLogger.Warn(content);
        }

        public void Warn(string content, params string[] tags)
        {
            _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Warn).AddTags(tags).Submit();
            _nLogger.Warn(content);
        }

        public void Warn(string content, string[] tags, params object[] datas)
        {
            var eventBuilder = _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Warn).AddTags(tags);
            foreach (var data in datas)
            {
                eventBuilder.AddObject(data);
            }
            eventBuilder.Submit();
            _nLogger.Warn(content);
        }

        public EventBuilder WarnBuilder(string content)
        {
            return _exless.CreateLog(content, Exceptionless.Logging.LogLevel.Warn);
        }
    }
}
