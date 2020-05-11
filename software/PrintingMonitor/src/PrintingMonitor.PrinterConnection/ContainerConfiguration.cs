using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.PrinterConnection.Connection;
using PrintingMonitor.PrinterConnection.Connection.Actions;
using PrintingMonitor.PrinterConnection.Connection.Configurator;
using PrintingMonitor.PrinterConnection.Sender;

namespace PrintingMonitor.PrinterConnection
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddPrinterConnection(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddPrinterConnectionSupport(configuration);
            services.AddConnectionActions();
            services.AddHostedService<PrinterCommandSenderService>();

            return services;
        }

        private static void AddPrinterConnectionSupport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISerialPortConfigurator, SerialPortConfigurator>();
            services.AddSingleton<SerialPrinterConnection>();
            services.AddSingleton(provider => provider.GetPrinterConnection());
            services.AddSingleton<ICommunicationConnection>(provider => provider.GetService<SerialPrinterConnection>());
            services.AddOptions().Configure<SerialConnectionOptions>(configuration.GetSection("SerialConnection"));
        }

        private static void AddConnectionActions(this IServiceCollection services)
        {
            services.AddSingleton<IOnConnectionAction, SetFirstLineAction>();
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
