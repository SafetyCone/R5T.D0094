using System;

using Microsoft.Extensions.Logging;

using R5T.T0064;


namespace R5T.D0094
{
    [ServiceDefinitionMarker]
    public interface IConsoleLoggerProvider : ILoggerProvider, IServiceDefinition
    {
    }
}