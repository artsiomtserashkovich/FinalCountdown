using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Management
{
    public class ReportTemperatures : Command
    {
        public ReportTemperatures() : base(CommandType.M, 105) { }
    }
}
