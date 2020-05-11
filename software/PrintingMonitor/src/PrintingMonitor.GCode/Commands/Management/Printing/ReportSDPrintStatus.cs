namespace PrintingMonitor.GCode.Commands.Management.Printing
{
    public class ReportSDPrintStatus : Command
    {
        public ReportSDPrintStatus() : base(CommandType.M, 27) { }
    }
}
