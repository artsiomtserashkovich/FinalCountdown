using System;

namespace PrintingMonitor.Printer.Connection
{
    public class InvalidConnectParametersException : Exception
    {
        public InvalidConnectParametersException(string message) : base(message) { }
    }
}
