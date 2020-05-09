using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintingMonitor.Printer.Models.Connection
{
    public class ConnectAvailableParameters
    {
        public ConnectAvailableParameters(IEnumerable<string> comPorts, IEnumerable<int> baudRates)
        {
            ComPorts = (comPorts ?? Array.Empty<string>()).ToList();
            BaudRates = (baudRates ?? Array.Empty<int>()).ToList();
        }

        public IReadOnlyCollection<int> BaudRates { get; }

        public IReadOnlyCollection<string> ComPorts { get; }
    }
}
