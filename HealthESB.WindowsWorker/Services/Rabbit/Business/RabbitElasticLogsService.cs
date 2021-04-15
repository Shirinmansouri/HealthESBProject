using HealthESB.RabbitMQ.Config;
using HealthESB.WindowsWorker.Config.Rabbit;
using HealthESB.WindowsWorker.Models;
using HealthESB.WindowsWorker.Services.ElasticSearch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Services.Rabbit.Business
{
    public class RabbitElasticLogsService : IRabbitBaseService
    {
        private RabbitQueue _rabbitEnum;
        private IModel _channel;
        private IElasticSearchService _elasticSearchService;
        private SeriLogSetting _seriLogSetting;
        private string logfile { get; set; }
        private string baseDir { get; set; }

        public RabbitElasticLogsService(RabbitQueue rabbitEnum, IModel channel, IOptions<SeriLogSetting> settings, IServiceProvider serviceProvider)
        {
            _rabbitEnum = rabbitEnum;
            _channel = channel;
            _elasticSearchService = serviceProvider.GetRequiredService<IElasticSearchService>();
            _seriLogSetting = settings.Value;
            baseDir = _seriLogSetting.baseDir;
            logfile = Path.Combine(baseDir, _seriLogSetting.App_Data, _seriLogSetting.logs, _seriLogSetting.log);
        }

        public void execute()
        {

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.With(new ThreadIdEnricher())
                        .Enrich.FromLogContext()
                        .WriteTo.Console(LogEventLevel.Information, _seriLogSetting.loggerTemplate, theme: AnsiConsoleTheme.Literate)
                        .WriteTo.File(logfile, LogEventLevel.Information, _seriLogSetting.loggerTemplate,
                            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                               .CreateLogger();


            try
            {
                Log.Information("====================================================================");
                Log.Information($"Start RabbitElasticLogsService:");
                Log.Information($"Application Directory: {_seriLogSetting.baseDir}");
                _channel.QueueDeclare(queue: _rabbitEnum.ToString(), durable: false, exclusive: false, autoDelete: true, arguments: null);
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += Consumer_Received;
                _channel.BasicConsume(queue: _rabbitEnum.ToString(), autoAck: true, consumer: consumer);
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

        public void Consumer_Received(object sender, BasicDeliverEventArgs e)
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
                Log.Information($"Start Consumer_Receiver For : ",e.ConsumerTag);
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.Information("====================================================================\r\n");
                Log.CloseAndFlush();
            }

        }


    }
}
