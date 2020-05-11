using System;
using System.Threading.Tasks;
using PrintingMonitor.Printer.Models.Commands;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Commands.Management;
using PrintingMonitor.Printer.Models.Information;
using PrintingMonitor.Printer.Notification;
using PrintingMonitor.Printer.Queues;

namespace PrintingMonitor.Printer
{
    internal class Printer : IPrinter, IDisposable
    {
        private readonly IInterservicesQueue<UserCommand> _userCommandQueue;
        private readonly INotificator<EndPrintingInformation> _endPrintingNotificator;

        public Printer(
            IInterservicesQueue<UserCommand> userCommandQueue, 
            INotificator<EndPrintingInformation> endPrintingNotificator)
        {
            _userCommandQueue = userCommandQueue;
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

            await _userCommandQueue.AddMessage(command);
        }

        public async Task ExecuteInformationCommand(InformationCommand command)
        {
            if (IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _userCommandQueue.AddMessage(command);
        }

        public async Task StartPrint(StartPrintCommand command)
        {
            if (IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _userCommandQueue.AddMessage(command);
        }

        public async Task StopPrint()
        {
            if (!IsPrinting)
            {
                throw new InvalidOperationException();
            }

            await _userCommandQueue.AddMessage(new StopPrintCommand());
        }

        private Task EndPrintingHandler(EndPrintingInformation information)
        {
            IsPrinting = false;

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _endPrintingNotificator.Unsubscribed(this);
        }
    }
}
