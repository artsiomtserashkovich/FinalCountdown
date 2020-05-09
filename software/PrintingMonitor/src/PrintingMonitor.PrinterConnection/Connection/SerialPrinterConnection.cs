using System;
using System.IO.Ports;
using System.Linq;
using Microsoft.Extensions.Options;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.Printer.Models.Connection;
using PrintingMonitor.PrinterConnection.Configurator;

namespace PrintingMonitor.PrinterConnection.Connection
{
    internal class SerialPrinterConnection : IPrinterConnection, IDisposable
    {
        private readonly SerialPort _port;
        private readonly IOptions<SerialConnectionOptions> _connectionOptions;
        private readonly ISerialPortConfigurator _configurator;

        public SerialPrinterConnection(IOptions<SerialConnectionOptions> connectionOptions, ISerialPortConfigurator configurator)
        {
            _connectionOptions = connectionOptions;
            _configurator = configurator;
            _port = new SerialPort();
            _configurator.ConfigureOnInitialize(_port, _connectionOptions.Value);
        }

        public bool IsConnected => _port.IsOpen;

        public void Connect(ConnectParameters parameters)
        {
            _configurator.ConfigureOnConnect(_port, parameters, _connectionOptions.Value);
            _port.Open();
        }

        public void Disconnect()
        {
            _port.Close();
        }

        public ConnectAvailableParameters GetConnectAvailableParameters()
        {
            return new ConnectAvailableParameters(
                SerialPort.GetPortNames().Where(_connectionOptions.Value.AllowedComPorts.Contains),
                _connectionOptions.Value.AllowedBaudRates);
        }

        public void Dispose()
        {
            _port?.Dispose();
        }
    }
}
