namespace PrintingMonitor.ResponseAnalyzer.Analyzers
{
    internal interface IResponseAnalyzersFactory
    {
        IResponseAnalyzer CreateResponseAnalyzer();
    }
}
