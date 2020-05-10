using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    internal class PositionMonitoringWorker : IMonitoringWorker
    {
        private readonly INotificationDispatcher<Positions> _positionsNotificationDispatcher;
        private readonly ILogger<PositionMonitoringWorker> _logger;
        private readonly TimeSpan _interval;
        private readonly IInterservicesQueue<Command> _queue;
        private Timer _timer;

        public PositionMonitoringWorker(
            TimeSpan interval,
            IInterservicesQueue<Command> queue,
            INotificationDispatcher<Positions> positionsNotificationDispatcher,
            ILogger<PositionMonitoringWorker> logger)
        {
            _interval = interval;
            _queue = queue;
            _positionsNotificationDispatcher = positionsNotificationDispatcher;
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
            if (_positionsNotificationDispatcher.HasSubscribers)
            {
                await _queue.AddMessage(new GetCurrentPosition());

                _logger.LogInformation($"{nameof(GetCurrentPosition)} was created.");
            }
        }
    }
}
