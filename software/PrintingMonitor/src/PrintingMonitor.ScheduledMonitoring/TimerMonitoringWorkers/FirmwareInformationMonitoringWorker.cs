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
    internal class FirmwareInformationMonitoringWorker : IMonitoringWorker
    {
        private readonly INotificationDispatcher<FirmwareInformation> _firmwareInformationNotificationDispatcher;
        private readonly ILogger<FirmwareInformationMonitoringWorker> _logger;
        private readonly TimeSpan _interval;
        private readonly IInterservicesQueue<Command> _queue;
        private Timer _timer;

        public FirmwareInformationMonitoringWorker(
            TimeSpan interval,
            IInterservicesQueue<Command> queue,
            INotificationDispatcher<FirmwareInformation> firmwareInformationNotificationDispatcher,
            ILogger<FirmwareInformationMonitoringWorker> logger)
        {
            _interval = interval;
            _queue = queue;
            _firmwareInformationNotificationDispatcher = firmwareInformationNotificationDispatcher;
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
            if (_firmwareInformationNotificationDispatcher.HasSubscribers)
            {
                await _queue.AddMessage(new ReadParamsFromEEPROM());

                _logger.LogInformation($"{nameof(ReadParamsFromEEPROM)} was created.");
            }
        }
    }
}
