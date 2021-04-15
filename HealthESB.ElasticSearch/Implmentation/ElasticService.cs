using HealthESB.ElasticSearch.Config;
using HealthESB.ElasticSearch.IContracts;
using HealthESB.RabbitMQ.IContract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HealthESB.ElasticSearch.Implmentation
{
    
    public class ElasticService: IElasticService
    {
        IConfiguration _configuration;
        private string baseUri = "";
        private IRabbitMqService _rabbitMqService;
        public ElasticService(IConfiguration configuration,IRabbitMqService rabbitMqService)
        {
            this._configuration = configuration;
            this._rabbitMqService = rabbitMqService;
            baseUri = this._configuration.GetSection("ConnectionStrings").GetSection("ElasticUri").Value;
        }

        public async System.Threading.Tasks.Task testElasticAsync(string Id)
        {
            var requrest=  (HttpWebRequest)WebRequest.Create(baseUri+ ElasticConfig.index+ Id);
            this._rabbitMqService.PublishToQueue(RabbitMQ.Config.RabbitQueue.ElasticLogs, "test elastic");
            var response =  (HttpWebResponse)(await requrest.GetResponseAsync().ConfigureAwait(true));
            this._rabbitMqService.PublishToQueue(RabbitMQ.Config.RabbitQueue.ElasticLogs, "test elastic");
        }
    }
}
