using System;

namespace Microliu.Logger
{
    public class ConsoleLogger : ILogger
    {
        string _ip;

        public ConsoleLogger()
        {
            _ip = MicroliuHelper.GetLocalIPAddress();
        }



        public void Debug(string content)
        {
            PrintWithColor(LogLevel.Debug, content);
        }

        public void Error(string content)
        {
            PrintWithColor(LogLevel.Error, content);
        }

        public void Info(string content)
        {
            PrintWithColor(LogLevel.Info, content);
        }

        public void Trace(string content)
        {
            PrintWithColor(LogLevel.Trace, content);
        }

        public void Warn(string content)
        {
            PrintWithColor(LogLevel.Warn, content);
        }



        private string GetDatetimeNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        private void PrintWithColor(LogLevel logLevel, string content)
        {
            var defaultColor = Console.ForegroundColor;
            ConsoleColor customColor = defaultColor;

            if (logLevel == LogLevel.Trace) customColor = ConsoleColor.DarkGreen;
            else if (logLevel == LogLevel.Debug) customColor = ConsoleColor.Green;
            else if (logLevel == LogLevel.Info) customColor = ConsoleColor.White;
            else if (logLevel == LogLevel.Warn) customColor = ConsoleColor.Yellow;
            else if (logLevel == LogLevel.Error) customColor = ConsoleColor.Red;

            Console.Write(GetDatetimeNow());
            Console.Write(" ");
            Console.ForegroundColor = customColor;
            Console.Write(typeof(LogLevel).Name);
            Console.Write(" ");
            Console.Write($"[{_ip}]");
            Console.Write(" ");
            Console.WriteLine(content);
        }

    }
}
