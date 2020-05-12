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
            
            return new PrinterResponseInformationAnalyzer(_provider, null);
        }
    }
}
