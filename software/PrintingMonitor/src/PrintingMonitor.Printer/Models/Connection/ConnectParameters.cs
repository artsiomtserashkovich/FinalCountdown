using System.ComponentModel.DataAnnotations;

namespace PrintingMonitor.Printer.Models.Connection
{
    public class ConnectParameters
    {
        [Required]
        public string ComPort { get; set; }

        [Required]
        public int BaudRate { get; set; }
    }
}
