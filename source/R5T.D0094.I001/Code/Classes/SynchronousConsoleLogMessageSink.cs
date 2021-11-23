using System;

using R5T.L0017.X002;
using R5T.T0087;


namespace R5T.D0094.I001
{
    public class SynchronousConsoleLogMessageSink : IConsoleLogMessageSink
    {
        private IConsole OutputConsole { get; }
        private IConsole ErrorConsole { get; }


        public SynchronousConsoleLogMessageSink(
            IConsole outputConsole,
            IConsole errorConsole)
        {
            this.OutputConsole = outputConsole;
            this.ErrorConsole = errorConsole;
        }

        public void Dispose()
        {
            // Nothing to dispose.
            GC.SuppressFinalize(this);
        }

        public void WriteLogMessage(ConsoleLogMessage message)
        {
            Instances.LoggerOperator.WriteLogMessageEntry(this.OutputConsole, this.ErrorConsole, message);
        }
    }
}
