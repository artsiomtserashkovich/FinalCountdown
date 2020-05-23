namespace PrintingMonitor.Printer.Models.Information
{
    public class Temperatures : ResponseInformation
    {
        public double? BedCurrent { get; set; }

        public double? BedTarget { get; set; }

        public double? HotendCurrent { get; set; }

        public double? HotendTarget { get; set; }
    }
}
