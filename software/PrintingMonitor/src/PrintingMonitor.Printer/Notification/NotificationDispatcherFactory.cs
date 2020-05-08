using System;
using Microsoft.Extensions.DependencyInjection;

namespace PrintingMonitor.Printer.Notification
{
    internal class NotificationDispatcherFactory<T> : INotificationDispatcherFactory<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationDispatcherFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public INotificationDispatcher<T> Create()
        {
            if (!(_serviceProvider.GetService<INotificator<T>>() is INotificationDispatcher<T> dispatcher))
            {
                throw new InvalidOperationException("INotificationDispatcher<T> isn't registered in DI.'");
            }

            return dispatcher;
        }
    }
}
