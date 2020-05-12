using System;

namespace PrintingMonitor.Printer.Models.Information
{
    public class ConnectionOutputInformation
    {
        public DateTime CommandSendTime { get; set; }

        public DateTime ResponseReceiveTime { get; set; }

        public string Command { get; set; }

        public string Response { get; set; }
    }
}
