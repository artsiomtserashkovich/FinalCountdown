using System;
using System.Threading.Tasks;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    public interface IMonitoringWorker : IDisposable
    {
        Task Start();
        Task Stop();
    }
}
