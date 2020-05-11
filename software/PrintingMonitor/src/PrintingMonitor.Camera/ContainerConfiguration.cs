using Microsoft.Extensions.DependencyInjection;

namespace PrintingMonitor.Camera
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddCameraSupport(this IServiceCollection services)
        {
            return services.AddHostedService<CameraCapturingService>();
        }
    }
}
