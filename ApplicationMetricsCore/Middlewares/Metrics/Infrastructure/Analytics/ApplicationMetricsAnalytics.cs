using ApplicationMetricsCore.Middlewares.Metrics.DataStore;
using ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.MetricsType;
using ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.Report;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.Analytics
{
    public class ApplicationMetricsAnalytics
    {
        public static void ReportingAppMetrics(int intervalTime)
         => new Task(() =>
          {
              while (true)
              {

                  ApplicationMetricsDataStore appDS = new ApplicationMetricsDataStore()
                  {
                      Timestamp = DateTime.Now,
                      ActiveRequests = CounterMetrics._countActiveRequests,
                      Errors5xx = CounterMetrics._countErrors5xx,
                      Errors4xx = CounterMetrics._countErrors4xx,
                      CpuUsage = GaugeMetrics.CpuUsage(),
                      PhysicalMemoryUsageMB = GaugeMetrics.MemoryUsage()
                  };

                  ElasticSearchReport.ReportingApplicationMetrics(appDS);

                  Thread.Sleep(intervalTime);
              }
          }, TaskCreationOptions.LongRunning)
            .Start();
    }

}
