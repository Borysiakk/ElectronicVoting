using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Validator.Infrastructure.Serilog
{
    public static class Extensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null)
        {

            void ConfigureLogger(WebHostBuilderContext context, LoggerConfiguration loggerConfiguration)
            {
                var name = Environment.GetEnvironmentVariable("COMPUTER_NAME");

                applicationName = name ?? "Validator";
                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(LogEventLevel.Information)
                    .Enrich.WithProperty("ApplicationName", applicationName)
                    .WriteTo.Console(
                        outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext} {Message}{NewLine}{Exception}",
                        theme: AnsiConsoleTheme.Code)
                    .WriteTo.Sentry(o =>
                    {
                        o.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                        o.MinimumEventLevel = LogEventLevel.Error;
                    });
            }
            return webHostBuilder
                .UseSerilog(ConfigureLogger)
                .UseSentry((c, o) =>
                {
                    o.Dsn = "https://aa096e87ea2a1260069f6fc26d5f1cb2@o4505766270009344.ingest.sentry.io/4505766272761856";
                    o.TracesSampleRate = 0.3;
                    o.MinimumBreadcrumbLevel = LogLevel.Debug;
                    o.MinimumEventLevel = LogLevel.Error;
                    o.AttachStacktrace = true;
                    o.MaxRequestBodySize = Sentry.Extensibility.RequestSize.Always; //this line
                    o.IncludeActivityData = true;
                    o.SendDefaultPii = true;
                });
        }


    }
}
