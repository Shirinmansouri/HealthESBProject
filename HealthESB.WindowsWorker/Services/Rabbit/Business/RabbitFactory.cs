using HealthESB.RabbitMQ.Config;
using HealthESB.WindowsWorker.Config.Rabbit;
using HealthESB.WindowsWorker.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Services.Rabbit.Business
{
    public static class RabbitFactory
    {
        public static IRabbitBaseService createInstance(RabbitQueue rabbitEnum, IModel channel, IOptions<SeriLogSetting> serLogSettings, IServiceProvider serviceProvider)
        {
            IRabbitBaseService instance = null;
            switch (rabbitEnum)
            {
                case RabbitQueue.ElasticLogs:
                    instance = new RabbitElasticLogsService(rabbitEnum, channel, serLogSettings, serviceProvider);
                    break;
                default:
                    break;
            }
            return instance;
        }
    }
}
