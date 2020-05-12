using System.Threading.Tasks;
using PrintingMonitor.GCode;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal interface IResponseAnalyzer
    {
        Task Analyze(PrinterResponse response);
    }
}
