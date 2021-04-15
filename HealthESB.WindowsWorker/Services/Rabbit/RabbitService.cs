using HealthESB.WindowsWorker.Config.Rabbit;
using HealthESB.WindowsWorker.Models;
using HealthESB.WindowsWorker.Services.ElasticSearch;
using HealthESB.WindowsWorker.Services.Rabbit.Business;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.RabbitMQ.Config;

namespace HealthESB.WindowsWorker.Services.Rabbit
{
    public class RabbitService : IRabbitService
    {
        private readonly AppSettingModel _settings;
        private readonly IServiceProvider _serviceProvider;
        private SeriLogSetting _seriLogSetting;
        private IOptions<SeriLogSetting> _seriLogSettingOptions;
        private string logfile { get; set; }
        private string baseDir { get; set; }
        public RabbitService(IOptions<AppSettingModel> settings, IOptions<SeriLogSetting> seriLogSetting, IServiceProvider serviceProvider)
        {
            _settings = settings.Value;
            _serviceProvider = serviceProvider;
            _seriLogSetting = seriLogSetting.Value;
            _seriLogSettingOptions = seriLogSetting;
        }
        public void runConsumer()
        {

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.With(new ThreadIdEnricher())
                        .Enrich.FromLogContext()
                        .WriteTo.Console(LogEventLevel.Information, _seriLogSetting.loggerTemplate, theme: AnsiConsoleTheme.Literate)
                        .WriteTo.File(logfile, LogEventLevel.Information, _seriLogSetting.loggerTemplate,
                            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                               .CreateLogger();

            try
            {
                Log.Information("====================================================================");
                Log.Information($"Start RabbitService:" +typeof(RabbitService).Name);
                Log.Information($"Application Directory: {_seriLogSetting.baseDir}");
                var factory = new ConnectionFactory()
                {
                    HostName = _settings.RabbitMqUri
                };
                using var connection = factory.CreateConnection();
                Log.Information($"Rabbit Connection Created:");
                using var channel = connection.CreateModel();
                Log.Information($"Rabbit Channel Created:");

                Log.Information($"Rabbit Enums Queue for Executing :", (RabbitQueue[])Enum.GetValues(typeof(RabbitQueue)));
                foreach (RabbitQueue item in (RabbitQueue[])Enum.GetValues(typeof(RabbitQueue)))
                {
                    Log.Information($"Rabbit {item.ToString()} Going to create instance from factory:");
                    var rabbitServiceInstace = RabbitFactory.createInstance(item, channel, _seriLogSettingOptions, _serviceProvider);
                    Log.Information($"Rabbit {item.ToString()} instance Created:");
                    rabbitServiceInstace.execute();
                    Log.Information($"Rabbit {item.ToString()} instance executed:");
                }//convert to parallel task
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
    }
}
