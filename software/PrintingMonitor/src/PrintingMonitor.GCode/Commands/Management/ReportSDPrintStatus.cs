using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Management
{
    public class ReportSDPrintStatus : Command
    {
        public ReportSDPrintStatus() : base(CommandType.M, 27) { }
    }
}
