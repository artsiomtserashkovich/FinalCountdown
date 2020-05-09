using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.ScheduledMonitoring
{
    internal class ScheduledMonitoringService : IHostedService, IDisposable
    {
        private readonly ILogger<ScheduledMonitoringService> _logger;
        private readonly ICameraCaptureQuery _query;
        private readonly IOptions<ScheduledMonitoringOptions> _options;
        private readonly INotificationDispatcher<Temperatures> _temperatureNotificationDispatcher;
        private readonly INotificationDispatcher<Positions> _positionsNotificationDispatcher;
        private readonly INotificationDispatcher<FirmwareInformation> _firmwareNotificationDispatcher;
        private readonly INotificationDispatcher<CameraCaptureImage> _cameraCaptureNotificationDispatcher;
        private Timer _timer;

        public ScheduledMonitoringService(
            ILogger<ScheduledMonitoringService> logger,
            ICameraCaptureQuery query,
            IOptions<ScheduledMonitoringOptions> options,
            INotificationDispatcherFactory<CameraCaptureImage> cameraCaptureNotificationDispatcherFactory,
            INotificationDispatcherFactory<Temperatures> temperatureNotificationDispatcherFactory,
            INotificationDispatcherFactory<Positions> positionsNotificationDispatcherFactory,
            INotificationDispatcherFactory<FirmwareInformation> firmwareNotificationDispatcherFactory)
        {
            _logger = logger;
            _query = query;
            _options = options;
            _cameraCaptureNotificationDispatcher = cameraCaptureNotificationDispatcherFactory.Create();
            _temperatureNotificationDispatcher = temperatureNotificationDispatcherFactory.Create();
            _positionsNotificationDispatcher = positionsNotificationDispatcherFactory.Create();
            _firmwareNotificationDispatcher = firmwareNotificationDispatcherFactory.Create();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ =>
                {
                    await DoWork();
                }, 
                null, 
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            var random = new Random();

            var pid = new PIDSettings
            {
                P = random.NextDouble(),
                I = random.NextDouble(),
                D = random.NextDouble(),
            };

            var home = new HomeOffsetSettings
            {
                X = random.NextDouble(),
                Y = random.NextDouble(),
                Z = random.NextDouble(),
            };

            var steps = new StepsPerUnitSettings
            {
                X = random.Next(),
                Y = random.Next(),
                Z = random.Next(),
                E = random.Next(),
            };

            var information = new FirmwareInformation
            {
                FilamentDiameter = random.NextDouble(),
                HomeOffset = home,
                PID = pid,
                StepsPerUnit = steps,
            };
            
            var positions = new Positions
            {
                E = random.NextDouble(),
                X = random.NextDouble(),
                Y = random.NextDouble(),
                Z = random.NextDouble(),
            };

            var tempratues = new Temperatures
            {
                BedCurrent = random.NextDouble(),
                BedTarget = random.NextDouble(),
                HotendCurrent = random.NextDouble(),
                HotendTarget = random.NextDouble(),
            };

            var camera = new CameraCaptureImage
            {
                Base64Content = _query.GetBase64Capture()
            };

            await _cameraCaptureNotificationDispatcher.Notification(camera);
            await _firmwareNotificationDispatcher.Notification(information);
            await _positionsNotificationDispatcher.Notification(positions);
            await _temperatureNotificationDispatcher.Notification(tempratues);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
