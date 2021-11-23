using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using R5T.D0093;
using R5T.T0063;


namespace R5T.D0094.I001
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddConsoleLogMessageSink(this IServiceCollection services,
            IServiceAction<ILoggerSynchronicityProvider> loggerSynchronicityProviderAction)
        {
            services
                .Run(loggerSynchronicityProviderAction)
                .TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>())
                ;

            return services;
        }
    }
}