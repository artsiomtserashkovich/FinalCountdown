using System.Collections.Generic;

namespace PrintingMonitor.Data
{
    public class StubPrinterConnection : IPrinterConnection
    {
        public bool IsConnected { get; set; }
        public IReadOnlyCollection<string> AvailablePorts { get; }
        public IReadOnlyCollection<int> AvailableBaudRate { get; }

        public StubPrinterConnection()
        {
            AvailableBaudRate = new[] { 9600, 19200, 38400, 230400, 115200, 250000,};
            AvailablePorts = new[] {"COM1", "COM2", "COM3"};
        }
    }
}
