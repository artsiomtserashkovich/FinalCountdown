using System.Collections.Generic;

namespace PrintingMonitor.GCode.Commands.Management
{
    public class SetCurrentLineNumber : Command
    {
        public SetCurrentLineNumber(int line) 
            : base(CommandType.M, 110, GetArguments(line))
        {
        }

        private static IEnumerable<string> GetArguments(int lineNumber)
        {
            yield return $"N{lineNumber}";
        }

        public static SetCurrentLineNumber GetFirstLineCommand()
        {
            return new SetCurrentLineNumber(1);
        }
    }
}
