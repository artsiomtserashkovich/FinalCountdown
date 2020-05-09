using System.Collections.Generic;

namespace PrintingMonitor.ScheduledMonitoring.TimerMonitoringWorkers
{
    internal interface ITimerMonitoringWorkersFactory
    {
        IEnumerable<IMonitoringWorker> CreateSingletonWorkers();

        IEnumerable<IMonitoringWorker> CreateScopedWorkers();
    }
}
