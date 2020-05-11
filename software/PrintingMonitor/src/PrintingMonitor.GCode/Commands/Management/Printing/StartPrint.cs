using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Management.Printing
{
    public class StartPrint : Command
    {
        public StartPrint() : base(CommandType.M, 24) { }
    }
}
