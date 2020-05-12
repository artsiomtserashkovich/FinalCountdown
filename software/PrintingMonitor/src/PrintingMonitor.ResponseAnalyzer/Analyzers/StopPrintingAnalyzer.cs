using System;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands.Management.Printing;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class StopPrintingAnalyzer : BaseResponseAnalyzer
    {
        public StopPrintingAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) : base(provider, nextAnalyzer)
        {
        }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            if (response.Command is StopPrint)
            {

                var dispatcher = GetNotificationDispatcher<EndPrintingInformation>();

                if (dispatcher.HasSubscribers)
                    await dispatcher.Notification(new EndPrintingInformation());

                return true;
            }

            return false;
        }
    }
}
