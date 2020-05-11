using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenCvSharp;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.Camera
{
    internal class CameraCapturingService : BackgroundService
    {
        private readonly ILogger<CameraCapturingService> _logger;
        private readonly IInterservicesQueue<GetCameraCapture> _queue;
        private readonly INotificationDispatcher<CameraCaptureImage> _cameraCaptureNotificationDispatcher;

        public CameraCapturingService(
            ILogger<CameraCapturingService> logger,
            IInterservicesQueue<GetCameraCapture> queue,
            INotificationDispatcherFactory<CameraCaptureImage> cameraCaptureNotificationDispatcherFactory)
        {
            _logger = logger;
            _queue = queue;
            _cameraCaptureNotificationDispatcher = cameraCaptureNotificationDispatcherFactory.Create();
        }
        
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service is starting.");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _queue.GetMessage();

                try
                {
                    _logger.LogInformation($"{nameof(GetCameraCapture)} was received.");

                    var cameraCapture = GetBase64Capture();

                    await _cameraCaptureNotificationDispatcher.Notification(cameraCapture);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error occurred during processing");
                }
            }
        }

        private static CameraCaptureImage GetBase64Capture()
        {
            using (var capture = new VideoCapture(0))
            using (var frame = new Mat())
            {
                capture.Read(frame);

                var base64Image = Convert.ToBase64String(frame.ToBytes());

                return new CameraCaptureImage
                {
                    Base64Content = base64Image
                };
            }
        }
    }
}
