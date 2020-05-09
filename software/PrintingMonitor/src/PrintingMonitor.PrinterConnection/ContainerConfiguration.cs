using System;
using System.Configuration.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.PrinterConnection.Configurator;
using PrintingMonitor.PrinterConnection.Connection;

namespace PrintingMonitor.PrinterConnection
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddPrinterConnection(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<ISerialPortConfigurator, SerialPortConfigurator>();
            services.AddSingleton<SerialPrinterConnection>();
            services.AddSingleton(provider => provider.GetPrinterConnection());
            services.AddOptions().Configure<SerialConnectionOptions>(configuration.GetSection("SerialConnection"));
            
            return services;
        }

        private static IPrinterConnection GetPrinterConnection(this IServiceProvider provider)
        {
            var connection = provider.GetService<SerialPrinterConnection>();
            var logger = provider.GetService<ILogger<IPrinterConnection>>();

            if (connection is null)
            {
                throw new InvalidOperationException();
            }
            if (logger is null)
            {
                throw new InvalidOperationException();
            }

            return new PrinterConnectionWithLogging(connection, logger);
        }
    }
}
