using System;
using System.Threading.Tasks;
using PrintingMonitor.Printer.Models;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.Data.Stubs
{
    public class StubFirmwareInformationQuery : IFirmwareInformationQuery
    {
        public Task<FirmwareInformation> Get()
        {
            var random = new Random();

            var pid = new PIDSettings
            {
                P = random.NextDouble(),
                I = random.NextDouble(),
                D = random.NextDouble(),
            };

            var home = new HomeOffsetSettings
            {
                X = random.NextDouble(),
                Y = random.NextDouble(),
                Z = random.NextDouble(),
            };

            var steps = new StepsPerUnitSettings
            {
                X = random.Next(),
                Y = random.Next(),
                Z = random.Next(),
                E = random.Next(),
            };

            var information = new FirmwareInformation
            {
                FilamentDiameter = random.NextDouble(),
                HomeOffset = home,
                PID = pid,
                StepsPerUnit = steps,
            };

            return Task.FromResult(information);
        }
    }
}
