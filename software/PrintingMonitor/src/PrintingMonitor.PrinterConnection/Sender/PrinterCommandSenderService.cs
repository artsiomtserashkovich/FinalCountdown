using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.Printer.Queues;
using PrintingMonitor.PrinterConnection.Connection;

namespace PrintingMonitor.PrinterConnection.Sender
{
    internal class PrinterCommandSenderService : BackgroundService
    {
        private readonly ICommunicationConnection _connection;
        private readonly ILogger<PrinterCommandSenderService> _logger;
        private readonly IInterservicesQueue<PrinterResponse> _responseQueue;
        private readonly IInterservicesQueue<Command> _commandQueue;

        public PrinterCommandSenderService(
            ICommunicationConnection connection,
            ILogger<PrinterCommandSenderService> logger,
            IInterservicesQueue<PrinterResponse> responseQueue,
            IInterservicesQueue<Command> commandQueue)
        {
            _connection = connection;
            _logger = logger;
            _responseQueue = responseQueue;
            _commandQueue = commandQueue;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service is starting.");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var command = await _commandQueue.GetMessage();

                _logger.LogInformation($"{command} was received.");

                var sendDate = DateTime.Now;
                _connection.SendCommand(command.ToString());

                var IsResponseReceived = false;
                do
                {
                    if (_connection.HasResponseToRead)
                    {
                        var response = _connection.ReadUntil("ok");
                        IsResponseReceived = true;
                        var receivedDate = DateTime.Now;

                        _logger.LogInformation($"{response} was received from printer.");

                        await _responseQueue.AddMessage(new PrinterResponse(sendDate, command, receivedDate, response));
                    }

                } while (!IsResponseReceived);
            }
        }
    }
}
