using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands;

namespace PrintingMonitor.PrinterConnection.Sender
{
    internal interface IPrinterCommandSender
    {
        PrinterResponse SendCommand(Command command);
    }
}
