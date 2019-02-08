using ApplicationMetricsCore.Middlewares.Metrics.DataStore;
using ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.MetricsType;
using ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.Report;
using Microsoft.AspNetCore.Http;
using System;

namespace ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.Analytics
{
    public class RequestMetricsAnalytics
    {
        public static RequestMetricsDataStore AddRequestMetricsDataStoreForReport(HttpContext context, string System)
            => new RequestMetricsDataStore
            {
                Timestamp = DateTime.Now,
                System = System,
                Host = context.Request.Host.Value,
                Endpoint = context.Request.Path.Value,
                CallType = context.Request.Method,
                TypeEndpoint = $"{context.Request.Path.Value} - {context.Request.Method}",
                StatusCode = context.Response.StatusCode,
                StatusCodeType = context.Response.StatusCode.ToString().StartsWith("1") ? "Information"
                    : context.Response.StatusCode.ToString().StartsWith("2") ? "Success"
                    : context.Response.StatusCode.ToString().StartsWith("3") ? "Redirection"
                    : context.Response.StatusCode.ToString().StartsWith("4") ? "Client Error"
                    : context.Response.StatusCode.ToString().StartsWith("5") ? "Server Error"
                    : "Other",
                RequestTimeMS = TimerMetrics.RequestTime,
                ResponseTimeMS = TimerMetrics.ResponseTime,
                UserCall = "eliabe" //context.Request.Headers.Get("Authorization");
            };

        public static void ReportingElasticSearch(RequestMetricsDataStore requestDS) 
            => ElasticSearchReport.ReportingRequestMetrics(requestDS);
    }
}
