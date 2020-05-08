using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrintingMonitor.ScheduledMonitoring
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddScheduledMonitoringSupport(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ScheduledMonitoringOptions>(configuration.GetSection("ScheduledMonitoring"));
            services.AddHostedService<ScheduledMonitoringService>();

            return services;
        }
    }
}
