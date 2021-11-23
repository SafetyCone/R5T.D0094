using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

using R5T.D0093;
using R5T.T0063;


namespace R5T.D0094.I001
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddConsole(this ILoggingBuilder loggingBuilder,
            IServiceAction<ILoggerSynchronicityProvider> loggerSynchronicityProviderAction)
        {
            loggingBuilder.AddConfiguration();

            loggingBuilder.Services.AddConsoleLogMessageSink(
                loggerSynchronicityProviderAction);

            return loggingBuilder;
        }
    }
}
