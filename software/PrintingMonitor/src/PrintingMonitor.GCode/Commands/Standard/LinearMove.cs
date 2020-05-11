using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingMonitor.GCode.Commands.Standard
{
    public class LinearMove : Command
    {
        public LinearMove(double x, double y, double z, double e) 
            : base(CommandType.G, 1, ParseArguments(x, y, z, e))
        {
        }
        
        private static IEnumerable<string> ParseArguments(double x, double y, double z, double e)
        {
            yield return $"X{x}";
            yield return $"Y{y}";
            yield return $"Z{z}";
            yield return $"E{e}";
        }
    }
}
