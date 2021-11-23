using System;

using Microsoft.Extensions.Logging;

using R5T.L0017.T002;
using R5T.L0017.X002;


namespace R5T.D0094.I001
{
    /// <summary>
    /// A console logger that outsources its synchronous vs. asynchronous logging to <see cref="IConsoleLogMessageSink"/>.
    /// The <see cref="Microsoft.Extensions.Logging.Console.ConsoleLogger"/> uses an asynchronous queue, which while great for production where speed is of the essence, is bad for debugging, since log messages may not have actually been output by the time a breakpoint is hit (especially in asynchronous code).
    /// </summary>
    public class ConsoleLogger : LoggerBase
    {
        private IConsoleLogMessageSink ConsoleLogMessageSink { get; }


        public ConsoleLogger(string categoryName, IConsoleLogMessageSink consoleLogMessageSink)
            : base(categoryName)
        {
            this.ConsoleLogMessageSink = consoleLogMessageSink;
        }

        public override void WriteMessage(LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            var consoleLogMessage = Instances.LoggerOperator.GetConsoleLogMessage(logLevel, logName, eventId, message, exception);

            this.ConsoleLogMessageSink.WriteLogMessage(consoleLogMessage);
        }
    }
}
