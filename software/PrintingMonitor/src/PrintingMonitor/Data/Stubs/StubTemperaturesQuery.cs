using System;
using System.Threading.Tasks;
using PrintingMonitor.Printer.Models;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.Data.Stubs
{
    public class StubTemperaturesQuery : ITemperaturesQuery
    {
        public Task<Temperatures> Get()
        {
            var random = new Random();
            var tempratues = new Temperatures
            {
                BedCurrent = random.NextDouble(),
                BedTarget = random.NextDouble(),
                HotendCurrent = random.NextDouble(),
                HotendTarget = random.NextDouble(),
            };

            return Task.FromResult(tempratues);
        }
    }
}
