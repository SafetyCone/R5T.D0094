using System;

using R5T.L0017.X002;


namespace R5T.D0094.I001
{
    public interface IConsoleLogMessageSink : IDisposable
    {
        void WriteLogMessage(ConsoleLogMessage consoleLogMessage);
    }
}
