using System;
using System.Threading.Tasks;
using PrintingMonitor.Printer.Models;
using PrintingMonitor.Printer.Queries;

namespace PrintingMonitor.Data.Stubs
{
    public class StubPositionsQuery : IPositionsQuery
    {
        public Task<Positions> Get()
        {
            var random = new Random();
            var positions = new Positions
            {
                E = random.NextDouble(),
                X = random.NextDouble(),
                Y = random.NextDouble(),
                Z = random.NextDouble(),
            };

            return Task.FromResult(positions);
        }
    }
}
