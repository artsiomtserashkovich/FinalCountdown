using System.Collections.Generic;

namespace PrintingMonitor.Printer.Models.Information
{
    public class PrintingFileInformation : ResponseInformation
    {
        public IReadOnlyCollection<string> Filenames { get; set; }
    }
}
