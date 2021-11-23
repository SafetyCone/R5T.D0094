using System;
using System.Collections.Concurrent;
using System.Threading;

using R5T.L0017.X002;
using R5T.T0087;


namespace R5T.D0094.I001
{
    // See: https://github.com/aspnet/Logging/blob/master/src/Microsoft.Extensions.Logging.Console/Internal/ConsoleLoggerProcessor.cs
    public class AsynchronousConsoleLogMessageSink : IConsoleLogMessageSink
    {
        private const int MaximumQueuedMessageCount = 1024;


        private BlockingCollection<ConsoleLogMessage> LogMessageCollection { get; } = new BlockingCollection<ConsoleLogMessage>(AsynchronousConsoleLogMessageSink.MaximumQueuedMessageCount);
        private Thread OutputThread { get; set; }
        private IConsole OutputConsole { get; }
        private IConsole ErrorConsole { get; }


        public AsynchronousConsoleLogMessageSink(
            IConsole outputConsole, 
            IConsole errorConsole)
        {
            this.OutputConsole = outputConsole;
            this.ErrorConsole = errorConsole;

            this.OutputThread = new Thread(this.ProcessMessageQueue)
            {
                Name = "Console logger queue proccessing thread",
                IsBackground = true,
            };
            this.OutputThread.Start();
        }

        public void Dispose()
        {
            this.LogMessageCollection.CompleteAdding();

            GC.SuppressFinalize(this);
        }

        public void WriteLogMessage(ConsoleLogMessage message)
        {
            // Adds a message to the queue rather than actually writing.

            // Check if the blocking collection is actually usable.
            if(!this.LogMessageCollection.IsAddingCompleted)
            {
                var added = this.LogMessageCollection.TryAdd(message);
                if(added)
                {
                    return;
                }
            }

            // If the blocking collection is unusable (the adding phase of its lifetime is completed, or it is full) then just spend the time to actually write the message.
            this.ActuallyWriteLogMessage(message);
        }

        private void ActuallyWriteLogMessage(ConsoleLogMessage message)
        {
            Instances.LoggerOperator.WriteLogMessageEntry(this.OutputConsole, this.ErrorConsole, message);
        }

        private void ProcessMessageQueue()
        {
            //try
            //{
                foreach (var message in this.LogMessageCollection.GetConsumingEnumerable())
                {
                    this.ActuallyWriteLogMessage(message);
                }
            //}
        }
    }
}
