using Prometheus;

namespace RateLimitingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddOpenTelemetry()
            //    .WithMetrics(builder => builder.AddPrometheusExporter().AddPrometheusHttpListener());
            //Action<ResourceBuilder> configureResource = r => r.AddService(
            //    serviceName: builder.Configuration.GetValue<string>("ServiceName"),
            //    serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
            //    serviceInstanceId: Environment.MachineName);

            //        builder.Services.AddOpenTelemetry()
            //.ConfigureResource(configureResource)
            //.WithMetrics(builder =>
            //{
            //    // Metrics

            //    // Ensure the MeterProvider subscribes to any custom Meters.
            //    builder
            //        .AddMeter("RateLimiting")
            //        .SetExemplarFilter(new TraceBasedExemplarFilter())
            //        .AddRuntimeInstrumentation()
            //        .AddHttpClientInstrumentation()
            //        .AddAspNetCoreInstrumentation();

            //    switch (histogramAggregation)
            //    {
            //        case "exponential":
            //            builder.AddView(instrument =>
            //            {
            //                return instrument.GetType().GetGenericTypeDefinition() == typeof(Histogram<>)
            //                    ? new Base2ExponentialBucketHistogramConfiguration()
            //                    : null;
            //            });
            //            break;
            //        default:
            //            // Explicit bounds histogram is the default.
            //            // No additional configuration necessary.
            //            break;
            //    }

            //    builder.AddPrometheusExporter();
            //});

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //var concurrencyPolicy = "Concurrency";
            //builder.Services.AddRateLimiter(_ => _
            //       .AddConcurrencyLimiter(policyName: concurrencyPolicy, options =>
            //       {
            //           options.PermitLimit = 20;
            //           options.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
            //           options.QueueLimit = 50;
            //       }));

            var app = builder.Build();
            //app.UseOpenTelemetryPrometheusScrapingEndpoint();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseRateLimiter();

            app.MapControllers();
            app.UseHttpMetrics();
            app.MapMetrics();

            app.Run();
        }
    }
}