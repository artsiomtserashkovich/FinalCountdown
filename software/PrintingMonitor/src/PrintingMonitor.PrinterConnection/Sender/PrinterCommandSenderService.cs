using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.PrinterConnection.Sender
{
    internal class PrinterCommandSenderService : BackgroundService
    {
        private readonly IPrinterCommandSender _commandSender;
        private readonly ILogger<PrinterCommandSenderService> _logger;
        private readonly IInterservicesQueue<PrinterResponse> _responseQueue;
        private readonly IInterservicesQueue<Command> _commandQueue;

        public PrinterCommandSenderService(
            IPrinterCommandSender commandSender,
            ILogger<PrinterCommandSenderService> logger,
            IInterservicesQueue<PrinterResponse> responseQueue,
            IInterservicesQueue<Command> commandQueue)
        {
            _commandSender = commandSender;
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

                if (command is null)
                {
                    throw new ArgumentNullException();
                }

                var response = _commandSender.SendCommand(command);
                _logger.LogInformation($"{response} was received.");

                await _responseQueue.AddMessage(response);
            }
        }
    }
}
