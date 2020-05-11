using System;
using System.Threading.Tasks;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Commands.Management;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.Printer
{
    internal class Printer : IPrinter
    {
        private readonly IInterservicesQueue<ManagementCommand> _managementQueue;
        private readonly IInterservicesQueue<InformationCommand> _informationQueue;
        private readonly INotificator<EndPrintingInformation> _endPrintingNotificator;

        public Printer(
            IInterservicesQueue<ManagementCommand> managementQueue, 
            IInterservicesQueue<InformationCommand> informationQueue, 
            INotificator<EndPrintingInformation> endPrintingNotificator)
        {
            _managementQueue = managementQueue;
            _informationQueue = informationQueue;
            _endPrintingNotificator = endPrintingNotificator;

            _endPrintingNotificator.Subscribed(this, EndPrintingHandler);
        }

        public bool IsPrinting { get; private set; } = false;

        public async Task ExecuteManagementCommand(ManagementCommand command)
        {
            if (IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _managementQueue.AddMessage(command);
        }

        public async Task ExecuteInformationCommand(InformationCommand command)
        {
            if (IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _informationQueue.AddMessage(command);
        }

        public async Task StartPrint(StartPrintCommand command)
        {
            if (IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _managementQueue.AddMessage(command);
        }

        public async Task StopPrint()
        {
            if (!IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _managementQueue.AddMessage(new StopPrintCommand());
        }

        private Task EndPrintingHandler(EndPrintingInformation information)
        {
            IsPrinting = false;

            return Task.CompletedTask;
        }
    }
}
