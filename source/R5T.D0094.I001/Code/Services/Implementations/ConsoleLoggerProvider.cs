using System;

using Microsoft.Extensions.Logging;

using R5T.D0093;
using R5T.T0064;
using R5T.T0087;


namespace R5T.D0094.I001
{
    [ServiceImplementationMarker]
    public class ConsoleLoggerProvider : IConsoleLoggerProvider, IServiceImplementation
    {
        #region Static

        private static IConsole OutputConsole { get; }
        private static IConsole ErrorConsole { get; }


        static ConsoleLoggerProvider()
        {
            ConsoleLoggerProvider.OutputConsole = Instances.ConsoleOperator.GetOperatingSystemSpecificConsole();
            ConsoleLoggerProvider.ErrorConsole = Instances.ConsoleOperator.GetOperatingSystemSpecificConsole(true);
        }

        private static void PerformFirstTimeSetup(ConsoleLoggerProvider consoleLoggerProvider)
        {
            SyncOverAsyncHelper.ExecuteSynchronously(async () =>
            {
                var loggerSynchronicity = await consoleLoggerProvider.LoggerSynchonicityProvider.GetLoggerSynchronicity();

                var isSynchronous = loggerSynchronicity.IsSynchronous();
                if (isSynchronous)
                {
                    consoleLoggerProvider.ConsoleLogMessageSink = new SynchronousConsoleLogMessageSink(
                        ConsoleLoggerProvider.OutputConsole,
                        ConsoleLoggerProvider.ErrorConsole);
                }
                else
                {
                    consoleLoggerProvider.ConsoleLogMessageSink = new AsynchronousConsoleLogMessageSink(
                        ConsoleLoggerProvider.OutputConsole,
                        ConsoleLoggerProvider.ErrorConsole);
                }
            });
        }

        private static void EnsureIsSetup(ConsoleLoggerProvider consoleLoggerProvider)
        {
            var isSetup = consoleLoggerProvider.ConsoleLogMessageSink is object;
            if (!isSetup)
            {
                ConsoleLoggerProvider.PerformFirstTimeSetup(consoleLoggerProvider);
            }
        }

        #endregion


        private ILoggerSynchronicityProvider LoggerSynchonicityProvider { get; }
        
        // Requires setup on first-time use.
        private IConsoleLogMessageSink ConsoleLogMessageSink { get; set; }


        public ConsoleLoggerProvider(
            ILoggerSynchronicityProvider loggerSynchonicityProvider)
        {
            this.LoggerSynchonicityProvider = loggerSynchonicityProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            ConsoleLoggerProvider.EnsureIsSetup(this);

            var output = new ConsoleLogger(
                categoryName,
                this.ConsoleLogMessageSink);

            return output;
        }

        public void Dispose()
        {
            this.ConsoleLogMessageSink.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}