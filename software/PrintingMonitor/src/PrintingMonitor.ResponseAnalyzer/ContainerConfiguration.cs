using Microsoft.Extensions.DependencyInjection;
using PrintingMonitor.ResponseAnalyzer.Analyzers;

namespace PrintingMonitor.ResponseAnalyzer
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddResponseAnalyzers(this IServiceCollection services)
        {
            services.AddSingleton<IResponseAnalyzersFactory, CoRResponseAnalyzersFactory>();
            return services.AddHostedService<ResponseAnalyzerService>();
        }
    }
}
