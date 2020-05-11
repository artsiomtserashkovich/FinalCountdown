using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.Printer.Models.Connection;

namespace PrintingMonitor.PrinterConnection.Connection
{
    internal class PrinterConnectionWithLogging : IPrinterConnection
    {
        private readonly IPrinterConnection _origin;
        private readonly ILogger<IPrinterConnection> _logger;

        public PrinterConnectionWithLogging(IPrinterConnection origin, ILogger<IPrinterConnection> logger)
        {
            _origin = origin;
            _logger = logger;
        }

        public bool IsConnected => _origin.IsConnected;

        public async Task Connect(ConnectParameters parameters)
        {
            try
            {
                await _origin.Connect(parameters);

                _logger.LogInformation($"{nameof(_origin.Connect)} was executed with parameters: ComPort:{parameters.ComPort}; BaudRate:{parameters.BaudRate}.");
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message}.StackTrace:{exception.StackTrace}.");

                throw;
            }
        }

        public void Disconnect()
        {
            try
            {
                _origin.Disconnect();

                _logger.LogInformation($"{nameof(_origin.Disconnect)} was executed.");
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message}.StackTrace:{exception.StackTrace}.");

                throw;
            }
        }

        public ConnectAvailableParameters GetConnectAvailableParameters() => _origin.GetConnectAvailableParameters();
    }
}
