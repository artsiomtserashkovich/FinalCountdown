using System.IO.Ports;
using PrintingMonitor.Printer.Models.Connection;

namespace PrintingMonitor.PrinterConnection.Connection.Configurator
{
    internal interface ISerialPortConfigurator
    {
        void ConfigureOnInitialize(SerialPort serialPort);

        void ConfigureOnConnect(SerialPort serialPort, ConnectParameters connectParameters, SerialConnectionOptions options);
    }
}
