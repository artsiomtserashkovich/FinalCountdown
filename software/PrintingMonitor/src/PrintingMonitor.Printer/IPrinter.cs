using System.Threading.Tasks;
using PrintingMonitor.Printer.Models.Commands.Informations;
using PrintingMonitor.Printer.Models.Commands.Management;

namespace PrintingMonitor.Printer
{
    public interface IPrinter
    {
        bool IsPrinting { get; }

        Task ExecuteManagementCommand(ManagementCommand command);

        Task ExecuteInformationCommand(InformationCommand command);

        Task StartPrint(StartPrintCommand command);

        Task StopPrint();
    }
}
