using HealthESB.WindowsWorker.Models;
using HealthESB.WindowsWorker.Services.ElasticSearch;
using HealthESB.WindowsWorker.Services.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var logfile = Path.Combine(baseDir, "App_Data", "logs", "log.txt");
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.With(new ThreadIdEnricher())
                        .Enrich.FromLogContext()
                        .WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
                        .WriteTo.File(logfile, LogEventLevel.Information, loggerTemplate,
                            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                               .CreateLogger();

            try
            {
                Log.Information("====================================================================");
                Log.Information($"Application Starts. Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
                Log.Information($"Application Directory: {AppDomain.CurrentDomain.BaseDirectory}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application terminated unexpectedly");
            }
            finally
            {
                Log.Information("====================================================================\r\n");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // configure the app here.
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AppSettingModel>(hostContext.Configuration.GetSection("AppSettings"));
                    services.Configure<SeriLogSetting>(hostContext.Configuration.GetSection("SeriLogSetting"));
                    services.AddHostedService<Worker>();
                    services.AddTransient<IElasticSearchService, ElasticSearchService>();
                    services.AddTransient<IRabbitService, RabbitService>();
                })
                    .UseSerilog();
    }
}
