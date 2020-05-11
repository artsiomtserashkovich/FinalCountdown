using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.PrinterConnection.Connection.Actions
{
    internal class SetFirstLineAction : IOnConnectionAction
    {
        private readonly IInterservicesQueue<Command> _queue;

        public SetFirstLineAction(IInterservicesQueue<Command> queue)
        {
            _queue = queue;
        }

        public async Task Execute()
        {
            await _queue.AddMessage(SetCurrentLineNumber.GetFirstLineCommand());
        }
    }
}
