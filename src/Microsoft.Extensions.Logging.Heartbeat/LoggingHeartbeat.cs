using System;
using System.ComponentModel;
using System.Timers;


namespace Microsoft.Extensions.Logging.Heartbeat
{
    public sealed class LoggingHeartbeat : IDisposable
    {
        private const double DEFAULT_INTERVAL = 60000; // 1 minute

        private readonly ILogger _logger;
        private readonly LogLevel _level;
        private readonly string _message;

        private readonly Timer _timer;

        internal LoggingHeartbeat(ILogger logger, LogLevel level, string message, TimeSpan? interval)
        {
            _logger = logger;
            _level = level;
            _message = message;

            _timer = new Timer(interval?.TotalMilliseconds ?? DEFAULT_INTERVAL);
            _timer.Elapsed += StartHeartbeat;
            _timer.Start();
        }

        private void StartHeartbeat(object sender, ElapsedEventArgs e)
        {
            _logger.Log(_level, _message);
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString() ?? string.Empty;
        }
    }
}