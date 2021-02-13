using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Heartbeat;

namespace Runner
{
    internal sealed class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<BackgroundRunner>();
                })
                .RunConsoleAsync();
        }
    }

    internal class BackgroundRunner : BackgroundService
    {
        private readonly ILogger<BackgroundRunner> _logger;

        public BackgroundRunner(ILogger<BackgroundRunner> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var logHeartbeat = _logger.Heartbeat(LogLevel.Warning, "foo", TimeSpan.FromSeconds(2));
            _logger.LogInformation("started");

            // a long running process
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);


            _logger.LogInformation("done");
        }
    }
}
