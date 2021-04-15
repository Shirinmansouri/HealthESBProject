using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Services.Rabbit
{
    public interface IRabbitBaseService
    {
        void execute();
        void Consumer_Received(object sender, BasicDeliverEventArgs e);
    }
}
