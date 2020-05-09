using System;

namespace PrintingMonitor.Printer.Models
{
    public class PrinterResponse
    {
        public DateTime CommandSendTime { get; set; }

        public DateTime ResponseReceiveTime { get; set; }

        public string Command { get; set; }

        public string Response { get; set; }
    }
}
