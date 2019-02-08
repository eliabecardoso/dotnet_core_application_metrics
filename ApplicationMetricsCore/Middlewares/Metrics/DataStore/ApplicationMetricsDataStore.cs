using System;

namespace ApplicationMetricsCore.Middlewares.Metrics.DataStore
{
    public class ApplicationMetricsDataStore
    {
        //public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int ActiveRequests { get; set; }
        public int Errors4xx { get; set; }
        public int Errors5xx { get; set; }
        public double CpuUsage { get; set; }
        public double PhysicalMemoryUsageMB { get; set; }
    }
}
