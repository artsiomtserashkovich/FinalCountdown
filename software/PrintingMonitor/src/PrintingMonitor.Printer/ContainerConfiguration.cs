using Microsoft.Extensions.DependencyInjection;
using PrintingMonitor.Printer.Notification;

namespace PrintingMonitor.Printer
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddPrinterCoreSupport(this IServiceCollection services)
        {
            services.AddNotificationDispatcher();

            return services;
        }

        private static void AddNotificationDispatcher(this IServiceCollection services)
        {
            services.AddSingleton(typeof(INotificationDispatcherFactory<>), typeof(NotificationDispatcherFactory<>));
            services.AddSingleton(typeof(INotificator<>), typeof(NotificationDispatcher<>));
        }
    }
}
