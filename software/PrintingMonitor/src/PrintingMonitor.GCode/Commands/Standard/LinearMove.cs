using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingMonitor.GCode.Commands.Standard
{
    public class LinearMove : Command
    {
        public LinearMove(double? x = null, double? y = null, double? z = null, double? e = null) 
            : base(CommandType.G, 1, ParseArguments(x, y, z, e))
        {
        }
        
        private static IEnumerable<string> ParseArguments(double? x, double? y, double? z, double? e)
        {
            if(!(x is null))
            {
                yield return $"X{x}";
            }

            if (!(y is null))
            {
                yield return $"Y{y}";
            }

            if (!(z is null))
            {
                yield return $"Z{z}";
            }

            if (!(e is null))
            {
                yield return $"E{e}";
            }
        }
    }
}
