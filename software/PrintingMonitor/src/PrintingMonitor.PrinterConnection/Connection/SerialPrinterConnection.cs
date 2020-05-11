using System;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.Printer.Models.Connection;
using PrintingMonitor.PrinterConnection.Connection.Configurator;

namespace PrintingMonitor.PrinterConnection.Connection
{
    internal class SerialPrinterConnection : IPrinterConnection, ICommunicationConnection, IDisposable
    {
        private readonly SerialPort _port;
        private readonly IOnConnectionAction _onConnectionAction;
        private readonly IOptions<SerialConnectionOptions> _connectionOptions;
        private readonly ISerialPortConfigurator _configurator;

        public SerialPrinterConnection(
            IOnConnectionAction onConnectionAction, 
            IOptions<SerialConnectionOptions> connectionOptions, 
            ISerialPortConfigurator configurator)
        {
            _onConnectionAction = onConnectionAction;
            _connectionOptions = connectionOptions;
            _configurator = configurator;

            _port = new SerialPort();
            _configurator.ConfigureOnInitialize(_port);
        }

        public bool IsConnected => _port.IsOpen;

        public async Task Connect(ConnectParameters parameters)
        {
            _configurator.ConfigureOnConnect(_port, parameters, _connectionOptions.Value);
            _port.Open();
            
            await _onConnectionAction.Execute();
        }

        public void Disconnect()
        {
            _port.Close();
        }

        public ConnectAvailableParameters GetConnectAvailableParameters()
        {
            return new ConnectAvailableParameters(
                SerialPort.GetPortNames().Where(_connectionOptions.Value.AllowedComPorts.Contains),
                _connectionOptions.Value.AllowedBaudRates.Distinct());
        }

        public void Dispose()
        {
            _port?.Dispose();
        }

        public void SendCommand(string command)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Connection is disconnect.");
            }

            _port.WriteLine(command);
        }

        public string ReadUntil(string key)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Serial Connection isn't open.");
            }

            return _port.ReadTo(key);
        }

        public bool HasResponseToRead => _port.BytesToRead != 0;
    }
}
