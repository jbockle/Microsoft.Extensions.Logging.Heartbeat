using System;
using Microsoft.Extensions.Logging.Heartbeat;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging
{
    public static class LoggerExtensions
    {
        public static LoggingHeartbeat Heartbeat(this ILogger logger, string message, TimeSpan? interval = null)
        {
            return Heartbeat(logger, LogLevel.Information, message, interval);
        }

        public static LoggingHeartbeat Heartbeat(this ILogger logger, LogLevel level, string message, TimeSpan? interval = null)
        {
            return new LoggingHeartbeat(logger, level, message, interval);
        }
    }
}
