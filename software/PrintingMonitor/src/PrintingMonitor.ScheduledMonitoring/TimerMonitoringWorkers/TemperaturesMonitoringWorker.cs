using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.GCode.Commands.Management.Temperature;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    internal class TemperaturesMonitoringWorker : IMonitoringWorker
    {
        private readonly INotificationDispatcher<Temperatures> _temperaturesNotificationDispatcher;
        private readonly ILogger<TemperaturesMonitoringWorker> _logger;
        private readonly TimeSpan _interval;
        private readonly IInterservicesQueue<Command> _queue;
        private Timer _timer;

        public TemperaturesMonitoringWorker(
            TimeSpan interval,
            IInterservicesQueue<Command> queue,
            INotificationDispatcher<Temperatures> temperaturesNotificationDispatcher,
            ILogger<TemperaturesMonitoringWorker> logger)
        {
            _interval = interval;
            _queue = queue;
            _temperaturesNotificationDispatcher = temperaturesNotificationDispatcher;
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task Start()
        {
            _timer = new Timer(async _ =>
                {
                    await BackgroundProcessing();
                },
                null,
                TimeSpan.Zero,
                _interval);

            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async Task BackgroundProcessing()
        {
            if (_temperaturesNotificationDispatcher.HasSubscribers)
            {
                await _queue.AddMessage(new ReportTemperatures());

                _logger.LogInformation($"{nameof(ReportTemperatures)} was created.");
            }
        }
    }
}
