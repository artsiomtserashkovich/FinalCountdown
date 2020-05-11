using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintingMonitor.GCode.Commands
{
    public abstract class Command
    {
        public CommandType CommandType { get; }

        public int CommandId { get; }

        public IReadOnlyCollection<string> Arguments { get; protected set; }

        protected Command(CommandType commandType, int commandId, IEnumerable<string> arguments = null)
        {
            CommandType = commandType;
            CommandId = commandId;
            Arguments = (arguments ?? Array.Empty<string>()).ToList();
        }

        public override string ToString()
        {
            var arguments = string.Join(" ", Arguments);

            return $"{CommandType.ToString()}{CommandId} {arguments}";
        }
    }
}
