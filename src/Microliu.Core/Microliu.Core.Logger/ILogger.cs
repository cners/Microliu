using Exceptionless;
using System;

namespace Microliu.Core.Loggers
{
    public interface ILogger
    {

        //ExceptionlessClient Exless { get; }

        void Trace(string content);

        void Trace(string content, params string[] tags);
        void Trace(string content, string[] tags, params object[] datas);

        

        void Debug(string content);

        void Debug(string content, params string[] tags);
        void Debug(string content, string[] tags, params object[] datas);

        void Info(string content);

        void Info(string content, params string[] tags);
        void Info(string content, string[] tags, params object[] datas);

        void Warn(string content);

        void Warn(string content, params string[] tags);
        void Warn(string content, string[] tags, params object[] datas);

        void Error(string content);

        void Error(string content, params string[] tags);
        void Error(string content, string[] tags, params object[] datas);


        //EventBuilder TraceBuilder(string content);
        //EventBuilder DebugBuilder(string content);
        //EventBuilder InfoBuilder(string content);
        //EventBuilder WarnBuilder(string content);
        //EventBuilder ErrorBuilder(string content);
        //EventBuilder ToException(Exception exception);
    }
}
