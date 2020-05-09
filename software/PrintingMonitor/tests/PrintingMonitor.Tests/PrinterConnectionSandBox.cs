using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PrintingMonitor.PrinterConnection;

namespace PrintingMonitor.Tests
{
    [TestFixture]
    public class PrinterConnectionSandBox
    {
        private SerialConnectionOptions _options;

        [SetUp]
        public void Setup()
        {
            _options = new SerialConnectionOptions
            {
                DtrEnable = true,
                NewLineSeparator = "/n",
                OneStopBits = true,
            };
        }

        [Test]
        public async Task SendCommand__()
        {
            var availableComPort = SerialPort.GetPortNames().First(x => x != "COM1");

            var port = new SerialPort(availableComPort, 115200, Parity.None)
            {
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                DtrEnable = true,
                NewLine = "\n"
            };

        }

        private string GetCommandWithCheckSumAndNumberLine(string command, int lineNumber)
        {
            return null;
        }
    }
}
