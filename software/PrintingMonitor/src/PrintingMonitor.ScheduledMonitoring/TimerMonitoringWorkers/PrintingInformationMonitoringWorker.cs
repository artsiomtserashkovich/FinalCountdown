using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.Printer;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    internal class PrintingInformationMonitoringWorker : IMonitoringWorker
    {
        private readonly INotificationDispatcher<PrintingInformation> _printingInformationNotificationDispatcher;
        private readonly ILogger<PrintingInformationMonitoringWorker> _logger;
        private readonly TimeSpan _interval;
        private readonly IPrinter _printer;
        private readonly IInterservicesQueue<Command> _queue;
        private Timer _timer;

        public PrintingInformationMonitoringWorker(
            TimeSpan interval,
            IPrinter printer,
            IInterservicesQueue<Command> queue,
            INotificationDispatcher<PrintingInformation> printingInformationNotificationDispatcher,
            ILogger<PrintingInformationMonitoringWorker> logger)
        {
            _interval = interval;
            _printer = printer;
            _queue = queue;
            _printingInformationNotificationDispatcher = printingInformationNotificationDispatcher;
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
            if (_printingInformationNotificationDispatcher.HasSubscribers && _printer.IsPrinting)
            {
                await _queue.AddMessage(new ReportSDPrintStatus());

                _logger.LogInformation($"{nameof(ReportSDPrintStatus)} was created.");
            }
        }
    }
}
