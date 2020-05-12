using System;
using System.Linq;
using PrintingMonitor.GCode.Commands;

namespace PrintingMonitor.PrinterConnection.Sender.Models
{
    internal class SendPrinterCommand
    {
        public DateTime SendTime { get; }

        public int Line { get; }

        public Command Command { get; }

        public SendPrinterCommand(int line, Command command, DateTime sendTime)
        {
            SendTime = sendTime;
            Line = line;
            Command = command;
        }

        public string ToStringIncludingChecksum()
        {
            return $"{this} *{GetChecksum()}";
        }

        public override string ToString()
        {
            return $"N{Line} {Command}";
        }

        private int GetChecksum()
        {
            var check = ToString().Aggregate(0, (current, ch) => current ^ (ch & 0xff));

            check ^= 32;

            return check;
        }
    }
}
