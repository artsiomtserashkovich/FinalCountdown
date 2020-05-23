using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintingMonitor.GCode;
using PrintingMonitor.Printer.Queues;
using PrintingMonitor.ResponseAnalyzer.Analyzers;

namespace PrintingMonitor.ResponseAnalyzer
{
    internal class ResponseAnalyzerService : BackgroundService
    {
        private readonly IResponseAnalyzer _responseAnalyzer;
        private readonly ILogger<ResponseAnalyzerService> _logger;
        private readonly IInterservicesQueue<PrinterResponse> _responseQueue;

        public ResponseAnalyzerService(
            IResponseAnalyzersFactory responseAnalyzersFactory,
            ILogger<ResponseAnalyzerService> logger,
            IInterservicesQueue<PrinterResponse> responseQueue)
        {
            _responseAnalyzer = responseAnalyzersFactory.CreateResponseAnalyzer();
            _logger = logger;
            _responseQueue = responseQueue;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service is starting.");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _responseQueue.GetMessage();

                if (response is null)
                {
                    throw new InvalidOperationException();
                }

                _logger.LogInformation($"{response} was received.");

                await _responseAnalyzer.Analyze(response);
            }
        }
    }
}
