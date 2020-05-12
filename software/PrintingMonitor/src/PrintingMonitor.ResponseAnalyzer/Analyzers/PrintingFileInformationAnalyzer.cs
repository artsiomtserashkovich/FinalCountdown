using System;
using System.Linq;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands.Management.Printing;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class PrintingFileInformationAnalyzer : BaseResponseAnalyzer
    {
        public PrintingFileInformationAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) : base(
            provider, nextAnalyzer)
        {
        }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            if (response.Command is ListSDCard)
            {

                var dispatcher = GetNotificationDispatcher<PrintingFileInformation>();

                if (dispatcher.HasSubscribers)
                    await dispatcher.Notification(ToPrintingFileInformation(response));

                return true;
            }

            return false;
        }

        private static PrintingFileInformation ToPrintingFileInformation(PrinterResponse response)
        {
            var rawFilenames = response.Response.Split('\n').ToList();

            if (rawFilenames.Count >= 4)
            {
                rawFilenames.Remove("");
                rawFilenames.Remove("");
                rawFilenames.Remove("Begin file list");
                rawFilenames.Remove("End file list");
            }

            return new PrintingFileInformation
            {
                Filenames = rawFilenames
            };
        }
    }
}
