using System.Collections.Generic;

namespace PrintingMonitor.PrinterConnection
{
    public class PrinterConnectionOptions
    {
        public PrinterConnectionOptions()
        {
            AllowedBaudRates = new[] {9600, 19200, 38400, 230400, 115200, 250000};
            NewLineSeparator = "/n";
            OneStopBits = true;
            DtrEnable = true;
        }

        public IReadOnlyCollection<int> AllowedBaudRates { get; set; }

        public string NewLineSeparator { get; set; }

        public bool OneStopBits { get; set; }

        public bool DtrEnable { get; set; }
    }
}
