using System;

namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal class CoRResponseAnalyzersFactory : IResponseAnalyzersFactory
    {
        private readonly IServiceProvider _provider;

        public CoRResponseAnalyzersFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IResponseAnalyzer CreateResponseAnalyzer()
        {
            //var printingInformationAnalyzer = new PrintingInformationAnalyzer(_provider);
            //var stopPrintingAnalyzer = new StopPrintingAnalyzer(_provider, printingInformationAnalyzer);
            var printingFileAnalyzer = new PrintingFileInformationAnalyzer(_provider, null);
            var temperaturesAnalyzer = new TemperaturesResponseAnalyzer(_provider, printingFileAnalyzer);
            var positionsAnalyzer = new PositionsResponseAnalyzer(_provider, temperaturesAnalyzer);
            var firmwareInformationAnalyzer = new FirmwareInformationResponseAnalyzer(_provider, positionsAnalyzer);
            return new PrinterResponseInformationAnalyzer(_provider, firmwareInformationAnalyzer);
        }
    }
}
