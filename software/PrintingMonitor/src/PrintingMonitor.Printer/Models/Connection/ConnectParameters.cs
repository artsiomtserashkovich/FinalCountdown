using System.ComponentModel.DataAnnotations;

namespace PrintingMonitor.Printer.Models.Connection
{
    public class ConnectParameters
    {
        public ConnectParameters(string comPort, int baudRate)
        {
            ComPort = comPort;
            BaudRate = baudRate;
        }

        [Required]
        public string ComPort { get; }

        [Required]
        public int BaudRate { get; set; }
    }
}
