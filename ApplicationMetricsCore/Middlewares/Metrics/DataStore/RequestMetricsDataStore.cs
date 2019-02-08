using System;

namespace ApplicationMetricsCore.Middlewares.Metrics.DataStore
{
    public class RequestMetricsDataStore
    {
        //public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string System { get; set; }
        public string Host { get; set; }
        public string Endpoint { get; set; }
        public string CallType { get; set; }
        public string TypeEndpoint { get; set; }
        public string UserCall { get; set; }
        public int StatusCode { get; set; }
        public string StatusCodeType { get; set; }
        public double RequestTimeMS { get; set; }
        public double ResponseTimeMS { get; set; }
        public long PhysicalMemoryUsageMB { get; set; }
        public double CpuUsage { get; set; }
    }
}
