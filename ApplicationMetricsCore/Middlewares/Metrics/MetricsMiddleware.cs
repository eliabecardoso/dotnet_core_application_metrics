using ApplicationMetricsCore.Middlewares.Metrics.DataStore;
using ApplicationMetricsCore.Middlewares.Metrics.Enum;
using ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.Analytics;
using ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.MetricsType;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ApplicationMetricsCore.Middlewares.Metrics
{
    public class MetricsMiddleware
    {
        private RequestDelegate _next { get; set; }
        private HttpContext _context { get; set; }
        private const string System = "SA";

        public MetricsMiddleware(RequestDelegate next)
        {
            RunReportingApplicationMetricsAsync(TimeSpan.FromSeconds(10).Milliseconds);
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            RequestHandle();
            await _next.Invoke(context);

            _context = context;
            ResponseHandle();
        }

        public void RequestHandle()
        {
            TimerMetrics.StartResponse();
            TimerMetrics.StartRequest();
            ApplicationMetricsHandle(EnumRequestResponse.Request);
        }

        public void ResponseHandle()
        {
            ApplicationMetricsHandle(EnumRequestResponse.Response);
            TimerMetrics.StopRequest();
            RequestMetricsHandle();
        }

        public void ApplicationMetricsHandle(EnumRequestResponse rr)
        {
            switch (rr)
            {
                case EnumRequestResponse.Request:
                    CounterMetrics.IncrementActiveRequests();

                    break;
                case EnumRequestResponse.Response:
                    CounterMetrics.DecrementActiveRequests();

                    if (_context.Response.StatusCode.ToString().StartsWith("4"))
                        CounterMetrics.IncrementError4xx();
                    else if (_context.Response.StatusCode.ToString().StartsWith("5"))
                        CounterMetrics.IncrementError5xx();
                    break;
                default:
                    break;
            }
        }

        public void RequestMetricsHandle()
        {
            RequestMetricsDataStore requestDS = RequestMetricsAnalytics.AddRequestMetricsDataStoreForReport(_context, System);
            RequestMetricsAnalytics.ReportingElasticSearch(requestDS);
        }

        private void RunReportingApplicationMetricsAsync(int intervalTime) 
            => ApplicationMetricsAnalytics.ReportingAppMetrics(intervalTime);
    }
}
