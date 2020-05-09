using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers;

namespace PrintingMonitor.ScheduledMonitoring
{
    internal class ScheduledMonitoringService : IHostedService, IDisposable
    {
        private readonly ITimerMonitoringWorkersFactory _workersFactory;
        private readonly ILogger<ScheduledMonitoringService> _logger;
        private IReadOnlyCollection<IMonitoringWorker> _singletonWorkers;

        public ScheduledMonitoringService(ITimerMonitoringWorkersFactory workersFactory, ILogger<ScheduledMonitoringService> logger)
        {
            _workersFactory = workersFactory;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is starting.");

            _singletonWorkers = _workersFactory.CreateSingletonWorkers().ToList();

            foreach (var worker in _singletonWorkers)
            {
                await worker.Start();
            }
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");

            if (_singletonWorkers != null)
            {
                foreach (var worker in _singletonWorkers)
                {
                    await worker.Stop();
                }
            }

        }

        public void Dispose()
        {
            if (_singletonWorkers != null)
            {
                foreach (var worker in _singletonWorkers)
                {
                    worker?.Dispose();
                }
            }
        }
    }
}
