using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Standard
{
    public class SetToRelativePositioning : Command
    {
        public SetToRelativePositioning() : base(CommandType.G, 91) { }
    }
}
