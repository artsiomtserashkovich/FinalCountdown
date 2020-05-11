using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Management.Temperature
{
    public class FanOn : Command
    {
        public FanOn(int speed) : base(CommandType.M, 106, ParseArguments(speed))
        {
        }

        private static IEnumerable<string> ParseArguments(int speed)
        {
            yield return $"S{speed}";
        }
    }
}
