using System;
using System.Diagnostics;

namespace ApplicationMetricsCore.Middlewares.Metrics.Infrastructure.MetricsType
{
    public static class GaugeMetrics
    {
        static DateTime lastTime { get; set; }
        static TimeSpan lastTotalProcessorTime { get; set; }
        static DateTime currentTime { get; set; }
        static TimeSpan currentTotalProcessorTime { get; set; }
        static Process appProcess { get; set; }
        static bool active { get; set; }
        
        public static double CpuUsage()
        {
            appProcess = Process.GetCurrentProcess();

            double cpuUsage = 0d;

            if (!active)
            {
                lastTime = DateTime.Now;
                lastTotalProcessorTime = appProcess.TotalProcessorTime;
                active = true;
            }
            else
            {
                currentTime = DateTime.Now;
                currentTotalProcessorTime = appProcess.TotalProcessorTime;

                cpuUsage = ((currentTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds)
                                                        / currentTime.Subtract(lastTime).TotalMilliseconds
                                                        / Convert.ToDouble(Environment.ProcessorCount));
                cpuUsage *= 100;

                //double memoryMb = (appProcess.WorkingSet64 / 1024f) / 1024f;
                //Console.WriteLine($"{appProcess.ProcessName} CPU: {cpuUsage}% --- Memory: {memoryMb} MB");

                lastTime = currentTime;
                lastTotalProcessorTime = currentTotalProcessorTime;
            }
            return cpuUsage;
        }

        private static double ConvertBytesToMegaBytes(long value)
        {
            return (value / 1024f) / 1024f;
        }

        public static double MemoryUsage()
        {
            appProcess = Process.GetCurrentProcess();
            return (appProcess.PrivateMemorySize64 / 1024f) / 1024f;
        }
    }
}
