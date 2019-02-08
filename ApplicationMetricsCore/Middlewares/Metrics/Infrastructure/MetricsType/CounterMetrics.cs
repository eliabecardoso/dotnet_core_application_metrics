using System;

namespace ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.MetricsType
{
    public static class CounterMetrics
    {
        public static int _countActiveRequests { get; private set; }
        public static int _countErrors4xx { get; private set; }
        public static int _countErrors5xx { get; private set; }

        public static void IncrementActiveRequests() => _countActiveRequests++;
        public static void DecrementActiveRequests() => _countActiveRequests--;
        public static void IncrementError4xx() => _countErrors4xx++;
        public static void IncrementError5xx() => _countErrors5xx++;
        public static int CountProcessors() => Environment.ProcessorCount;
    }
}
