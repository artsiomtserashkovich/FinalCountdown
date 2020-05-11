using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Standard
{
    public class SetToAbsolutePositioning : Command 
    {
        public SetToAbsolutePositioning() : base(CommandType.G, 90) { }
    }
}
