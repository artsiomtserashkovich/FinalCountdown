using System.Threading.Tasks;
using PrintingMonitor.Printer.Models.Connection;

namespace PrintingMonitor.Printer.Connection
{
    public interface IPrinterConnection
    {
        bool IsConnected { get; }

        Task Connect(ConnectParameters parameters);

        void Disconnect();

        ConnectAvailableParameters GetConnectAvailableParameters();
    }
}
