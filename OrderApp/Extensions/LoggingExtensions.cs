using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using Serilog.Exceptions;

namespace OrderApp.API.Extensions
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddLoggingExtension(this IServiceCollection services)
        {

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .Enrich.WithExceptionDetails()
                 //.WriteTo.Debug()
                 //.WriteTo.Console()
                 .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                 .Enrich.WithProperty("Environment", environment)
                 .ReadFrom.Configuration(configuration)
                 .CreateLogger();

            ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
            {
                return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                };
            }

            return services;
        }
    }
}
