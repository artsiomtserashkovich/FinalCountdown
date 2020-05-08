using System;

namespace PrintingMonitor.ScheduledMonitoring
{
    public class ScheduledMonitoringOptions
    {
        public ScheduledMonitoringOptions()
        {
            Positions = TimeSpan.FromSeconds(1);
            Temperatures = TimeSpan.FromSeconds(10);
            PrintingInformation = TimeSpan.FromSeconds(15);
        }

        public TimeSpan PrintingInformation { get; set; }

        public TimeSpan Temperatures { get; set; }

        public TimeSpan Positions { get; set; }
    }
}
