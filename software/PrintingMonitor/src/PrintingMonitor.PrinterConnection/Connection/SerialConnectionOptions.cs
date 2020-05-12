using System;

namespace PrintingMonitor.PrinterConnection.Connection
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
            RetryOnTimeout = 10;
            RetryOnError = 2;
            ReadTimeout = TimeSpan.FromSeconds(10);
        }

        public string[] AllowedComPorts { get; set; }

        public int[] AllowedBaudRates { get; set; }

        public string NewLineSeparator { get; set; }

        public bool OneStopBits { get; set; }

        public bool DtrEnable { get; set; }

        public int RetryOnTimeout { get; set; }

        public int RetryOnError { get; set; }

        public TimeSpan ReadTimeout { get; set; }
    }
}
