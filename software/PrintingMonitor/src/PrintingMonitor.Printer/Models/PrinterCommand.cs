using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingMonitor.Printer.Models
{
    class PrinterCommand
    {
        public DateTime CommandSendTime { get; set; }
        
        public string Command { get; set; }
    }
}
