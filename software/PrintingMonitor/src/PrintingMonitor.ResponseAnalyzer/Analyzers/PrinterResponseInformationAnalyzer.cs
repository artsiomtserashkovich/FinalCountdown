using System;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class PrinterResponseInformationAnalyzer : BaseResponseAnalyzer
    {
        public PrinterResponseInformationAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) 
            : base(provider, nextAnalyzer) { }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            var dispatcher = GetNotificationDispatcher<ConnectionOutputInformation>();

            if (dispatcher.HasSubscribers)
            {
                await dispatcher.Notification(ToInformation(response));
            }

            return false;
        }

        private ConnectionOutputInformation ToInformation(PrinterResponse response)
        {
            return new ConnectionOutputInformation
            {
                Response = response.Response,
                Command = response.Command.ToString(),
                CommandSendTime = response.SendTime,
                ResponseReceiveTime = response.ReceiveTime
            };
        }
    }
}
