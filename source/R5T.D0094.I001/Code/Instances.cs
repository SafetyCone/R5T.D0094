using System;

using R5T.T0086;
using R5T.T0088;


namespace R5T.D0094.I001
{
    public static class Instances
    {
        public static IConsoleOperator ConsoleOperator { get; } = T0088.ConsoleOperator.Instance;
        public static ILoggerOperator LoggerOperator { get; } = T0086.LoggerOperator.Instance;
    }
}
