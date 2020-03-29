using System.Collections.Generic;

namespace PrintingMonitor.Data
{
    public interface IPrinterConnection
    {
        bool IsConnected { get; set; }

        IReadOnlyCollection<string> AvailablePorts { get; }

        IReadOnlyCollection<int> AvailableBaudRate { get; }
    }
}
