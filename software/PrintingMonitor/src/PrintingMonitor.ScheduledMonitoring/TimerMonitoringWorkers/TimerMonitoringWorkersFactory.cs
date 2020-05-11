using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.Printer;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    internal class TimerMonitoringWorkersFactory : ITimerMonitoringWorkersFactory
    {
        private readonly IOptions<ScheduledMonitoringOptions> _options;
        private readonly IServiceProvider _serviceProvider;

        public TimerMonitoringWorkersFactory(IOptions<ScheduledMonitoringOptions> options, IServiceProvider serviceProvider)
        {
            _options = options;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IMonitoringWorker> CreateSingletonWorkers()
        {
            yield return new CameraCapturingMonitoringWorker(
                _options.Value.CameraCapturing,
                GetInterservicesQueue<GetCameraCapture>(),
                GetNotificationDispatcher<CameraCaptureImage>(),
                GetLogger<CameraCapturingMonitoringWorker>());
            
            yield return new PositionMonitoringWorker(
                _options.Value.Positions,
                GetInterservicesQueue<Command>(),
                GetNotificationDispatcher<Positions>(),
                GetLogger<PositionMonitoringWorker>());

            yield return new TemperaturesMonitoringWorker(
                _options.Value.Temperatures,
                GetInterservicesQueue<Command>(),
                GetNotificationDispatcher<Temperatures>(),
                GetLogger<TemperaturesMonitoringWorker>());

            yield return new PrintingInformationMonitoringWorker(
                _options.Value.PrintingInformation,
                GetPrinter(),
                GetInterservicesQueue<Command>(),
                GetNotificationDispatcher<PrintingInformation>(),
                GetLogger<PrintingInformationMonitoringWorker>());
        }
        
        private INotificationDispatcher<T> GetNotificationDispatcher<T>() where T : class
        {
            var dispatcherFactory = _serviceProvider.GetService(typeof(INotificationDispatcherFactory<T>)) as INotificationDispatcherFactory<T>;

            if (dispatcherFactory is null)
            {
                throw new InvalidOperationException($"Cant resolve {nameof(INotificationDispatcherFactory<T>)} from DI.");
            }

            return dispatcherFactory.Create();
        }

        private IInterservicesQueue<T> GetInterservicesQueue<T>() where T : class
        {
            var queue = _serviceProvider.GetService(typeof(IInterservicesQueue<T>)) as IInterservicesQueue<T>;

            if (queue is null)
            {
                throw new InvalidOperationException($"Cant resolve {nameof(IInterservicesQueue<T>)} from DI.");
            }

            return queue;
        }
        
        private ILogger<T> GetLogger<T>() where T : class
        {
            var logger = _serviceProvider.GetService(typeof(ILogger<T>)) as ILogger<T>;

            if (logger is null)
            {
                throw new InvalidOperationException($"Cant resolve {nameof(ILogger<T>)} from DI.");
            }

            return logger;
        }

        private IPrinter GetPrinter()
        {
            var printer = _serviceProvider.GetService(typeof(IPrinter)) as IPrinter;

            if (printer is null)
            {
                throw new InvalidOperationException($"Cant resolve {nameof(IPrinter)} from DI.");
            }

            return printer;
        }
    }
}
