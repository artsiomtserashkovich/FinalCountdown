using System.Collections.Generic;

namespace PrintingMonitor.PrinterConnection
{
    public class SerialConnectionOptions
    {
        public SerialConnectionOptions()
        {
            AllowedComPorts = new[] {"Com2", "Com3", "Com4", "Com5", "Com6"};
            AllowedBaudRates = new[] {9600, 19200, 38400, 230400, 115200, 250000};
            NewLineSeparator = "/n";
            OneStopBits = true;
            DtrEnable = true;
        }

        public IReadOnlyCollection<string> AllowedComPorts { get; set; }

        public IReadOnlyCollection<int> AllowedBaudRates { get; set; }

        public string NewLineSeparator { get; set; }

        public bool OneStopBits { get; set; }

        public bool DtrEnable { get; set; }
    }
}
