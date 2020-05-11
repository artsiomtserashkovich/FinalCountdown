using System;

namespace PrintingMonitor.Printer.Models.Commands.Management
{
    public class StartPrintCommand : ManagementCommand
    {
        public StartPrintCommand(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException(nameof(filename));
            }

            Filename = filename;
        }

        public string Filename { get; }
    }
}
