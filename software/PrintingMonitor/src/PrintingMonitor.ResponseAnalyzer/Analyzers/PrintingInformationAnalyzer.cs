using System;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands.Management.Printing;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    class PrintingInformationAnalyzer : BaseResponseAnalyzer
    {
        public PrintingInformationAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) : base(provider, nextAnalyzer)
        {
        }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            if (response.Command is ReportSDPrintStatus)
            {

                var dispatcher = GetNotificationDispatcher<PrintingInformation>();

                if (dispatcher.HasSubscribers)
                    await dispatcher.Notification(ToPrintingInformation(response.Response));

                return true;
            }

            return false;
        }

        private PrintingInformation ToPrintingInformation(string response)
        {
            return new PrintingInformation
            {
                Total = 100,
                Current = 10
            };
        }
    }
}
