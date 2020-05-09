using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Management
{
    public class ReadParamsFromEEPROM : Command
    {
        public ReadParamsFromEEPROM() : base(CommandType.M, 501) { }
    }
}
