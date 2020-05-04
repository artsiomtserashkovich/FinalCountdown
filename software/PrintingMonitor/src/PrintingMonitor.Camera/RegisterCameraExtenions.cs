using System;
using Microsoft.Extensions.DependencyInjection;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.Camera
{
    public static class ContainerCameraExtenions
    {
        public static IServiceCollection AddCameraSupport(this IServiceCollection services)
        {
            return services.AddSingleton<ICameraCaptureQuery, OpenCVCameraCaptureQuery>();
        }
    }
}
