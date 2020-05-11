using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    internal class CameraCapturingMonitoringWorker : IMonitoringWorker
    {
        private readonly INotificationDispatcher<CameraCaptureImage> _cameraCaptureNotificationDispatcher;
        private readonly ILogger<CameraCapturingMonitoringWorker> _logger;
        private readonly TimeSpan _interval;
        private readonly IInterservicesQueue<GetCameraCapture> _queue;
        private Timer _timer;

        public CameraCapturingMonitoringWorker(
            TimeSpan interval, 
            IInterservicesQueue<GetCameraCapture> queue, 
            INotificationDispatcher<CameraCaptureImage> cameraCaptureNotificationDispatcher,
            ILogger<CameraCapturingMonitoringWorker> logger)
        {
            _interval = interval;
            _queue = queue;
            _cameraCaptureNotificationDispatcher = cameraCaptureNotificationDispatcher;
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
            if (_cameraCaptureNotificationDispatcher.HasSubscribers)
            {
                await _queue.AddMessage(new GetCameraCapture());

                _logger.LogInformation($"{nameof(GetCameraCapture)} was created.");
            }
        }
    }
}
