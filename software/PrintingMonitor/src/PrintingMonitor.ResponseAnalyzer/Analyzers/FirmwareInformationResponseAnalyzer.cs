using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PrintingMonitor.GCode;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.Printer.Models.Information;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class FirmwareInformationResponseAnalyzer : BaseResponseAnalyzer
    {
        private const string StepsRegex =
            "echo: M92 X(?<X>[0-9]+,[0-9]+) Y(?<Y>[0-9]+,[0-9]+) Z(?<Z>[0-9]+,[0-9]+) E(?<E>[0-9]+,[0-9]+)";

        private const string PIDRegex =
            "echo:  M301 P(?<P>[0-9]+,[0-9]+) I(?<I>[0-9]+,[0-9]+) D(?<D>[0-9]+,[0-9]+)";

        private const string HomeOffsetRegex =
            "echo:  M206 X(?<X>[0-9]+,[0-9]+) Y(?<Y>[0-9]+,[0-9]+) Z(?<Z>[0-9]+,[0-9]+)";

        public FirmwareInformationResponseAnalyzer(IServiceProvider provider, IResponseAnalyzer nextAnalyzer = null) : base(provider, nextAnalyzer)
        {
        }

        protected override async Task<bool> HandleResponse(PrinterResponse response)
        {
            if (response.Command is ReadParamsFromEEPROM)
            {
                
                var dispatcher = GetNotificationDispatcher<FirmwareInformation>();

                if (dispatcher.HasSubscribers)
                    await dispatcher.Notification(ToFirmwareInformation(response));

                return true;
            }

            return false;
        }

        private static FirmwareInformation ToFirmwareInformation(PrinterResponse response)
        {
            return new FirmwareInformation
            {
                FilamentDiameter = 1.75,
                StepsPerUnit = ParseSteps(response.Response),
                PID = ParsePid(response.Response),
                HomeOffset = ParseHomeOffset(response.Response)
            };
        }

        private static PIDSettings ParsePid(string response)
        {
            var pid = new PIDSettings();
            var pidRegex = new Regex(PIDRegex);

            var matches = pidRegex.Match(response.Replace('.', ','));

            if (matches.Success)
            {
                var result = matches.Groups["P"];

                if (double.TryParse(matches.Groups["P"].Value, out double p))
                {
                    pid.P = p;
                }

                if (double.TryParse(matches.Groups["I"].Value, out double i))
                {
                    pid.I = i;
                }

                if (double.TryParse(matches.Groups["D"].Value, out double d))
                {
                    pid.D = d;
                }
            }

            return pid;
        }

        private static HomeOffsetSettings ParseHomeOffset(string response)
        {
            var homeOffset = new HomeOffsetSettings();
            var homeOffsetRegex = new Regex(HomeOffsetRegex);

            var matches = homeOffsetRegex.Match(response.Replace('.', ','));

            if (matches.Success)
            {
                if (double.TryParse(matches.Groups["X"].Value, out double x))
                {
                    homeOffset.X = x;
                }

                if (double.TryParse(matches.Groups["Y"].Value, out double y))
                {
                    homeOffset.Y = y;
                }

                if (double.TryParse(matches.Groups["Z"].Value, out double z))
                {
                    homeOffset.Z = z;
                }
            }

            return homeOffset;
        }

        private static StepsPerUnitSettings ParseSteps(string response)
        {
            var stepsSettings = new StepsPerUnitSettings();
            var stepsRegex = new Regex(StepsRegex);

            var matches = stepsRegex.Match(response.Replace('.', ','));

            if (matches.Success)
            {
                if (double.TryParse(matches.Groups["X"].Value, out double xSteps))
                {
                    stepsSettings.X = (int)xSteps;
                }

                if (double.TryParse(matches.Groups["E"].Value, out double ySteps))
                {
                    stepsSettings.Y = (int)ySteps;
                }

                if (double.TryParse(matches.Groups["Z"].Value, out double zSteps))
                {
                    stepsSettings.Z = (int)zSteps;
                }

                if (double.TryParse(matches.Groups["E"].Value, out double eSteps))
                {
                    stepsSettings.E = (int)eSteps;
                }
            }

            return stepsSettings;
        }
    }
}
