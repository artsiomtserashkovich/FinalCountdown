using Microsoft.Extensions.DependencyInjection;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.Data.Stubs
{
    public static class RegisterStubsExtenions
    {
        public static IServiceCollection RegisterStubs(this IServiceCollection services)
        {
            services.AddSingleton<IPrinterConnection, StubPrinterConnection>();

            return services;
        }
    }
}
