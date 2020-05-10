using System;
using System.Collections.Concurrent;
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
        private readonly IOptions<SerialConnectionOptions> _connectionOptions;
        private readonly ISerialPortConfigurator _configurator;
        private readonly ConcurrentDictionary<object, Func<string, Task>> _consumers = new ConcurrentDictionary<object, Func<string, Task>>();

        public SerialPrinterConnection(IOptions<SerialConnectionOptions> connectionOptions, ISerialPortConfigurator configurator)
        {
            _connectionOptions = connectionOptions;
            _configurator = configurator;
            _port = new SerialPort();
            _configurator.ConfigureOnInitialize(_port);
            _port.DataReceived += HandleDataReceived;
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

        public void SubscribedToResponse(Func<string, Task> handler, object key)
        {
            _consumers.TryAdd(key, handler);
        }

        private void HandleDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = sender as SerialPort;

            while (port.BytesToRead != 0)
            {
                var response = port.ReadTo("ok");

                foreach (var consumer in _consumers)
                {
                    consumer.Value(response).Wait();
                }
            }

        }
    }
}
