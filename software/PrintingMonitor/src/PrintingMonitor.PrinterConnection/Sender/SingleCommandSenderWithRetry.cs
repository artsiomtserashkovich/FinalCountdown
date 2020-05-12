using System;
using Microsoft.Extensions.Options;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.PrinterConnection.Connection;
using PrintingMonitor.PrinterConnection.Sender.Models;

namespace PrintingMonitor.PrinterConnection.Sender
{
    internal class SingleCommandSenderWithRetry : IPrinterCommandSender
    {
        private readonly ICommunicationConnection _connection;
        private readonly IOptions<SerialConnectionOptions> _options;
        private int _nextCommandLine = 1;

        public SingleCommandSenderWithRetry(ICommunicationConnection connection, IOptions<SerialConnectionOptions> _options)
        {
            _connection = connection;
            this._options = _options;
        }

        public PrinterResponse SendCommand(Command command)
        {

            var sendCommand = new SendPrinterCommand(_nextCommandLine, command, DateTime.Now);
            _connection.SendCommand(sendCommand.ToStringIncludingChecksum());

            var response = command.WaitResponseUntilOK ? _connection.ReadUntil("ok") : ReadWithoutOK();
            IncrementCommandLine();

            return new PrinterResponse(sendCommand.SendTime, sendCommand.Command, DateTime.Now, response);
        }

        private void IncrementCommandLine()
        {
            ++_nextCommandLine;
        }

        private string ReadWithoutOK()
        {
            _connection.ReadLine();
           return _connection.ReadLine();
        }
    }
}
