using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class PositionsResponseAnalyzer : BaseResponseAnalyzer
    {
        private const string PositionsRegex =
            "X:(?<X>[0-9]+,[0-9]+) Y:(?<Y>[0-9]+,[0-9]+) Z:(?<Z>[0-9]+,[0-9]+) E:(?<E>[0-9]+,[0-9]+)";

        public PositionsResponseAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) : base(provider, nextAnalyzer)
        {
        }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            if (response.Command is GetCurrentPosition)
            {

                var dispatcher = GetNotificationDispatcher<Positions>();

                if (dispatcher.HasSubscribers)
                    await dispatcher.Notification(ToPositionsInformation(response));

                return true;
            }

            return false;
        }

        private static Positions ToPositionsInformation(PrinterResponse response)
        {
            var positions = new Positions();
            var positionsRegex = new Regex(PositionsRegex);

            var matches = positionsRegex.Match(response.Response.Replace('.', ','));

            if (matches.Success)
            {
                var result = matches.Groups["P"];

                if (double.TryParse(matches.Groups["X"].Value, out double x))
                {
                    positions.X = x;
                }

                if (double.TryParse(matches.Groups["Y"].Value, out double y))
                {
                    positions.Y = y;
                }

                if (double.TryParse(matches.Groups["Z"].Value, out double z))
                {
                    positions.Z = z;
                }

                if (double.TryParse(matches.Groups["E"].Value, out double e))
                {
                    positions.E = e;
                }
            }

            return positions;
        }
    }
}
