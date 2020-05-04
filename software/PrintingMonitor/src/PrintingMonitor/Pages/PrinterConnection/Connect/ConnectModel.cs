using System.ComponentModel.DataAnnotations;

namespace PrintingMonitor.Pages.PrinterConnection.Connect
{
    public class ConnectModel
    {
        [Required]
        [ComPortNameValidation(ErrorMessage = "Invalid Value for ComPort Name.")]
        public string ComPortName { get; set; }

        [Required]
        [BaudRateValidation(ErrorMessage = "Invalid Value for BaudRate.")]
        public int BaudRate { get; set; }
    }
}
