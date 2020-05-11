using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.Printer.Models.Commands;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Commands.Management;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.CommandTranslation
{
    public class CommandTranslationService : BackgroundService
    {
        private readonly ILogger<CommandTranslationService> _logger;
        private readonly IInterservicesQueue<UserCommand> _userCommandQueue;
        private readonly IInterservicesQueue<Command> _commandQueue;

        private readonly ManagementCommandTranslator _managementCommandTranslator;
        private readonly InformationCommandTranslator _informationCommandTranslator;

        public CommandTranslationService(
            ILogger<CommandTranslationService> logger,
            IInterservicesQueue<UserCommand> userCommandQueue,
            IInterservicesQueue<Command> commandQueue)
        {
            _logger = logger;
            _userCommandQueue = userCommandQueue;
            _commandQueue = commandQueue;

            _managementCommandTranslator = new ManagementCommandTranslator();
            _informationCommandTranslator = new InformationCommandTranslator();
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
                var userCommand = await _userCommandQueue.GetMessage();

                try
                {
                    _logger.LogInformation($"{userCommand.GetType().Name} was received.");

                    IReadOnlyCollection<Command> translatedCommands = Array.Empty<Command>();

                    switch (userCommand)
                    {
                        case ManagementCommand managementCommand:
                            translatedCommands = _managementCommandTranslator.Translate(managementCommand);
                            break;
                        case InformationCommand informationCommand:
                            translatedCommands = _informationCommandTranslator.Translate(informationCommand);
                            break;
                    }

                    if (!translatedCommands.Any())
                    {
                        throw new InvalidOperationException(nameof(translatedCommands) + " is empty.");
                    }

                    foreach (var command in translatedCommands)
                    {
                        await _commandQueue.AddMessage(command);

                        _logger.LogInformation($"{command} command was translated");
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error occurred during processing");
                }
            }
        }
    }
}
