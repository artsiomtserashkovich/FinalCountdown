using System;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.Printer.Notification;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal abstract class BaseResponseAnalyzer : IResponseAnalyzer
    {
        private readonly IServiceProvider _provider;
        private readonly IResponseAnalyzer _nextAnalyzer;

        protected BaseResponseAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null)
        {
            _provider = provider;
            _nextAnalyzer = nextAnalyzer;
        }

        public async Task Analyze(PrinterResponse response)
        {
            var isOver = await HandleResponse(response);

            if (!isOver && _nextAnalyzer != null)
            {
                await _nextAnalyzer.Analyze(response);
            }
        }

        protected abstract Task<bool> HandleResponse(PrinterResponse response);

        protected INotificationDispatcher<T> GetNotificationDispatcher<T>() where T : class
        {
            var dispatcherFactory = _provider.GetService(typeof(INotificationDispatcherFactory<T>)) as INotificationDispatcherFactory<T>;

            if (dispatcherFactory is null)
            {
                throw new InvalidOperationException($"Cant resolve {nameof(INotificationDispatcherFactory<T>)} from DI.");
            }

            return dispatcherFactory.Create();
        }
    }
}
