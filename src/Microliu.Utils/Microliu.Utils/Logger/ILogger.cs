namespace Microliu.Logger
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
