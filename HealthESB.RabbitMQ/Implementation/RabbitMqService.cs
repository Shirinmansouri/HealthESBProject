using HealthESB.Framework.Utility;
using HealthESB.RabbitMQ.Config;
using HealthESB.RabbitMQ.IContract;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthESB.RabbitMQ.Implementation
{

    public class RabbitMqService: IRabbitMqService
    {
        IConfiguration _configuration;
        private string baseUri = "";
        public RabbitMqService(IConfiguration configuration)
        {
            this._configuration = configuration;
            baseUri = this._configuration.GetSection("ConnectionStrings").GetSection("RabbitMqUri").Value;
        }

        public void PublishToQueue(RabbitQueue queue, string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = baseUri
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: Utilities.GetParentType(queue), durable: false, exclusive: false, autoDelete: true, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                    routingKey: Utilities.GetParentType(queue),
                    basicProperties: null,
                    body: body);
        }
    }
}
