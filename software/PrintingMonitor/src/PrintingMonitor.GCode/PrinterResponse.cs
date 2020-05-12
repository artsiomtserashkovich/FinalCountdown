using System;
using PrintingMonitor.GCode.Commands;

namespace PrintingMonitor.GCode
{
    public class PrinterResponse
    {
        public DateTime SendTime { get; }

        public Command Command { get; }

        public DateTime ReceiveTime { get; }

        public string Response { get; }

        public PrinterResponse(DateTime sendTime, Command command, DateTime receiveTime, string response)
        {
            SendTime = sendTime;
            Command = command;
            ReceiveTime = receiveTime;
            Response = response;
        }

        public override string ToString()
        {
            return $"{SendTime}:{Command}; {ReceiveTime}:{Response}.";
        }
    }
}
