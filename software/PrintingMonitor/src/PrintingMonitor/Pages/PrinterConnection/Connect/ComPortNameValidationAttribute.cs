using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrintingMonitor.Pages.PrinterConnection.Connect
{
    public class ComPortNameValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedComPortName = new[]
        {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
        };

        public override bool IsValid(object value)
        {
            var comPortName = value as string;

            if (!(comPortName is null))
            {
                return _allowedComPortName.Contains(comPortName);
            }

            return false;
        }
    }
}
