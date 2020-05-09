using System.Collections.Generic;

namespace PrintingMonitor.PrinterConnection
{
    public class SerialConnectionOptions
    {
        public SerialConnectionOptions()
        {
            AllowedComPorts = new[] { "COM2", "COM3", "COM4" };
            AllowedBaudRates = new[] { 115200, 250000 };
            NewLineSeparator = "/n";
            OneStopBits = true;
            DtrEnable = true;
        }

        public string[] AllowedComPorts { get; set; }

        public int[] AllowedBaudRates { get; set; }

        public string NewLineSeparator { get; set; }

        public bool OneStopBits { get; set; }

        public bool DtrEnable { get; set; }
    }
}
