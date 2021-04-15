using HealthESB.WindowsWorker.Models;
using HealthESB.WindowsWorker.Services.Rabbit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<AppSettingModel> _settings;
        private readonly IRabbitService _rabbitService;

        public Worker(ILogger<Worker> logger, IOptions<AppSettingModel> settings, IRabbitService rabbitService)
        {
            _logger = logger;
            _settings = settings;
            _rabbitService = rabbitService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _rabbitService.runConsumer();
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _logger.LogInformation("Disposing Service");
            base.Dispose();
        }
    }
}
