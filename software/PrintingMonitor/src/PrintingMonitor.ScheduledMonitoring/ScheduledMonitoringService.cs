using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PrintingMonitor.Printer.Models;
using PrintingMonitor.Printer.Notification;

namespace PrintingMonitor.ScheduledMonitoring
{
    internal class ScheduledMonitoringService : IHostedService, IDisposable
    {
        private readonly IOptions<ScheduledMonitoringOptions> _options;
        private readonly INotificationDispatcher<Temperatures> _temperatureNotificationDispatcher;
        private readonly INotificationDispatcher<Positions> _positionsNotificationDispatcher;
        private readonly INotificationDispatcher<FirmwareInformation> _firmwareNotificationDispatcher;
        private Timer _timer;

        public ScheduledMonitoringService(
            IOptions<ScheduledMonitoringOptions> options,
            INotificationDispatcherFactory<Temperatures> temperatureNotificationDispatcher,
            INotificationDispatcherFactory<Positions> positionsNotificationDispatcher,
            INotificationDispatcherFactory<FirmwareInformation> firmwareNotificationDispatcher)
        {
            _options = options;
            _temperatureNotificationDispatcher = temperatureNotificationDispatcher.Create();
            _positionsNotificationDispatcher = positionsNotificationDispatcher.Create();
            _firmwareNotificationDispatcher = firmwareNotificationDispatcher.Create();
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

            await _firmwareNotificationDispatcher.ReceiveForNotification(information);
            await _positionsNotificationDispatcher.ReceiveForNotification(positions);
            await _temperatureNotificationDispatcher.ReceiveForNotification(tempratues);
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
