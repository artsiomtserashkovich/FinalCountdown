using System;
using Microsoft.Extensions.DependencyInjection;

namespace PrintingMonitor.Printer.Notification
{
    class NotificationDispatcherFactory<T> : INotificationDispatcherFactory<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationDispatcherFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public INotificationDispatcher<T> Get()
        {
            var dispatcher = _serviceProvider.GetService<NotificationDispatcher<T>>();

            if (dispatcher is null)
            {
                throw new InvalidOperationException("INotificationDispatcher<T> isn't registered in DI.'");
            }

            return dispatcher;
        }
    }
}
