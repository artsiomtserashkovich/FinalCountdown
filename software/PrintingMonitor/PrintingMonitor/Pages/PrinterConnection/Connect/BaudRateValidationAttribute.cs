using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrintingMonitor.Pages.PrinterConnection.Connect
{
    public class BaudRateValidationAttribute : ValidationAttribute
    {
        private readonly int[] _allowedBaudRate = new[]
        {
            9600, 19200, 38400, 230400, 115200, 250000
        };

        public override bool IsValid(object value)
        {
            var baudRate = (int)value;

            return _allowedBaudRate.Contains(baudRate);
        }
    }
}
