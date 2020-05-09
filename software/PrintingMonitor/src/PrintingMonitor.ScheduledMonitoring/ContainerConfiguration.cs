using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers;

namespace PrintingMonitor.ScheduledMonitoring
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddScheduledMonitoringSupport(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ITimerMonitoringWorkersFactory, TimerMonitoringWorkersFactory>();
            services.AddOptions().Configure<ScheduledMonitoringOptions>(configuration.GetSection("ScheduledMonitoring"));
            services.AddHostedService<ScheduledMonitoringService>();

            return services;
        }
    }
}
