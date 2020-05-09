using PrintingMonitor.Printer.Models.Connection;

namespace PrintingMonitor.Printer.Connection
{
    public interface IPrinterConnection
    {
        bool IsConnected { get; }

        void Connect(ConnectParameters parameters);

        void Disconnect();

        ConnectAvailableParameters GetConnectAvailableParameters();
    }
}
