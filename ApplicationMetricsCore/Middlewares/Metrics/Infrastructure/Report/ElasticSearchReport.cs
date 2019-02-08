using ApplicationMetricsCore.Middlewares.Metrics.DataStore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.Report
{
    public class ElasticSearchReport
    {
        public ElasticSearchReport()
        {

        }

        public static void ReportingRequestMetrics(RequestMetricsDataStore requestDS)
        {
            String json = JsonConvert.SerializeObject(requestDS);
            Byte[] bytes = new ASCIIEncoding().GetBytes(json);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://localhost:9200/metrics/_doc"));
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/json";

            Stream newStream = request.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            //var stream = response.GetResponseStream();
            //var sr = new StreamReader(stream);
            //var content = sr.ReadToEnd();
        }

        public static void ReportingApplicationMetrics(ApplicationMetricsDataStore appDS)
        {
            String json = JsonConvert.SerializeObject(appDS);
            Byte[] bytes = new ASCIIEncoding().GetBytes(json);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://localhost:9200/metricsapp/_doc"));
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/json";

            Stream newStream = request.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            //var stream = response.GetResponseStream();
            //var sr = new StreamReader(stream);
            //var content = sr.ReadToEnd();
        }
    }
}
