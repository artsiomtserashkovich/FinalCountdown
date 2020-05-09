using Microsoft.Extensions.DependencyInjection;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.Printer
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddPrinterCoreSupport(this IServiceCollection services)
        {
            services.AddNotificationDispatcher();
            services.AddQueueSupport();

            return services;
        }

        private static void AddNotificationDispatcher(this IServiceCollection services)
        {
            services.AddSingleton(typeof(INotificationDispatcherFactory<>), typeof(NotificationDispatcherFactory<>));
            services.AddSingleton(typeof(INotificator<>), typeof(NotificationDispatcher<>));
        }

        private static void AddQueueSupport(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IInterservicesQueue<>), typeof(InterservicesQueue<>));
        }
    }
}
