using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.Printer.Queues;
using PrintingMonitor.PrinterConnection.Connection;

namespace PrintingMonitor.PrinterConnection.Sender
{
    internal class PrinterCommandSenderService : BackgroundService
    {
        private readonly ICommunicationConnection _connection;
        private readonly ILogger<PrinterCommandSenderService> _logger;
        private readonly IInterservicesQueue<Command> _queue;

        public PrinterCommandSenderService(
            ICommunicationConnection connection,
            ILogger<PrinterCommandSenderService> logger,
            IInterservicesQueue<Command> queue)
        {
            _connection = connection;
            _logger = logger;
            _queue = queue;
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
                var command = await _queue.GetMessage();

                _connection.SendCommand(command.ToString());

                _logger.LogInformation($"{command} was received.");
            }
        }
    }
}
