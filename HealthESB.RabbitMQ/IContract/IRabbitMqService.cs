using HealthESB.RabbitMQ.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthESB.RabbitMQ.IContract
{
    public interface IRabbitMqService
    {
       void PublishToQueue(RabbitQueue queue, string message);

    }
}
