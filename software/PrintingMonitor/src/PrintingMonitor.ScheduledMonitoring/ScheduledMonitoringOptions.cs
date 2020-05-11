using System;

namespace PrintingMonitor.ScheduledMonitoring
{
    public class ScheduledMonitoringOptions
    {
        public ScheduledMonitoringOptions()
        {
            Positions = TimeSpan.FromSeconds(20);
            Temperatures = TimeSpan.FromSeconds(50);
            PrintingInformation = TimeSpan.FromSeconds(50);
            CameraCapturing = TimeSpan.FromSeconds(30);
        }

        public TimeSpan PrintingInformation { get; set; }

        public TimeSpan Temperatures { get; set; }

        public TimeSpan Positions { get; set; }

        public TimeSpan CameraCapturing { get; set; }
    }
}
