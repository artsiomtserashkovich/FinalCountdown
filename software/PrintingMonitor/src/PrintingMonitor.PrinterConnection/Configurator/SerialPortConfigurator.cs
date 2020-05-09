using System;
using System.IO.Ports;
using System.Linq;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.Printer.Models.Connection;

namespace PrintingMonitor.PrinterConnection.Configurator
{
    internal class SerialPortConfigurator : ISerialPortConfigurator
    {
        public void ConfigureOnInitialize(SerialPort serialPort)
        {
            AssertSerialPort(serialPort);

            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
        }

        public void ConfigureOnConnect(SerialPort serialPort, ConnectParameters connectParameters, SerialConnectionOptions options)
        {
            AssertSerialPort(serialPort);
            AssertConnectParameters(connectParameters, options);

            serialPort.BaudRate = connectParameters.BaudRate;
            serialPort.PortName = connectParameters.ComPort;
            serialPort.DtrEnable = options.DtrEnable;
            serialPort.NewLine = options.NewLineSeparator;
            serialPort.StopBits = options.OneStopBits? StopBits.One: StopBits.None;
        }

        private static void AssertConnectParameters(
            ConnectParameters connectParameters,
            SerialConnectionOptions options)
        {
            if (!options.AllowedComPorts.Contains(connectParameters?.ComPort))
            {
                throw new InvalidConnectParametersException($"Invalid value of ComPort: {connectParameters?.ComPort}");
            }

            if (!options.AllowedBaudRates.Contains(connectParameters?.BaudRate ?? 0))
            {
                throw new InvalidConnectParametersException($"Invalid value of BaudRate: {connectParameters?.BaudRate}");
            }
        }

        private static void AssertSerialPort(SerialPort serialPort)
        {
            if (serialPort is null)
            {
                throw new ArgumentNullException(nameof(serialPort));
            }

            if (serialPort.IsOpen)
            {
                throw new InvalidOperationException("Serial Port already connected.");
            }
        }
    }
}
