using Microsoft.Extensions.DependencyInjection;

namespace PrintingMonitor.CommandTranslation
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddUserCommandTranslation(this IServiceCollection services)
        {
            return services.AddHostedService<CommandTranslationService>();
        }
    }
}
