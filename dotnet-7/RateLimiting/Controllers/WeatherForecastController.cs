using Microsoft.AspNetCore.Mvc;
using System;

namespace RateLimitingAPI.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Route("/")]
        [HttpGet]
        public async Task<string> Get(int index)
        {
            await Task.Delay(1000);
            string GetTicks = (DateTime.Now.Ticks & 0x11111).ToString("00000");
            return $"index:{index}, Concurrency Limiter {GetTicks}, {DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}";
        }

        [Route("/ThreadCount")]
        [HttpGet]
        public ThreadCount ThreadCount()
        {
            ThreadCount threadCount = new();
            ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            threadCount.MinWorkerThreads = workerThreads;
            threadCount.MinCompletionPortThreads = completionPortThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            threadCount.MaxWorkerThreads = workerThreads;
            threadCount.MaxCompletionPortThreads = completionPortThreads;
            threadCount.ThreadCount1 = ThreadPool.ThreadCount;
            ThreadPool.SetMinThreads(100, 100);
            ThreadPool.SetMaxThreads(1000, 1000);
            return threadCount;
        }
    }

    public class ThreadCount
    {
        public int MinWorkerThreads { get; set; }
        public int MinCompletionPortThreads { get; set; }
        public int MaxWorkerThreads { get; set; }
        public int MaxCompletionPortThreads { get; set; }
        public int ThreadCount1 { get; set; }
    }
}