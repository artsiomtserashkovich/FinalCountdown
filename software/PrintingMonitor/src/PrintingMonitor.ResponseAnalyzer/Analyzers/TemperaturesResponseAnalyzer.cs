using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands.Management.Temperature;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class TemperaturesResponseAnalyzer : BaseResponseAnalyzer
    {
        private const string TemperaturesRegex = "T:(?<HCurrent>-?[0-9]+.[0-9]+) /(?<HTarget>[0-9]+.[0-9]+) B:(?<BCurrent>-?[0-9]+.[0-9]+) /(?<BTarget>[0-9]+.[0-9]+)";
        public TemperaturesResponseAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) : base(provider, nextAnalyzer)
        {
        }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            if (response.Command is ReportTemperatures)
            {

                var dispatcher = GetNotificationDispatcher<Temperatures>();

                if (dispatcher.HasSubscribers)
                    await dispatcher.Notification(ToTemperatures(response));

                return true;
            }

            return false;
        }

        private static Temperatures ToTemperatures(PrinterResponse response)
        {
            var temperatures = new Temperatures();
            var temperaturesRegex = new Regex(TemperaturesRegex);

            var matches = temperaturesRegex.Match(response.Response.Replace('.', ','));

            if (matches.Success)
            {
                var result = matches.Groups["P"];

                if (double.TryParse(matches.Groups["BCurrent"].Value, out double bc))
                {
                    temperatures.BedCurrent = bc;
                }

                if (double.TryParse(matches.Groups["BTarget"].Value, out double bt))
                {
                    temperatures.BedTarget = bt;
                }

                if (double.TryParse(matches.Groups["HCurrent"].Value, out double hc))
                {
                    temperatures.HotendCurrent = hc;
                }

                if (double.TryParse(matches.Groups["HTarget"].Value, out double ht))
                {
                    temperatures.HotendTarget = ht;
                }
            }

            return temperatures;
        }
    }
}
