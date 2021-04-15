using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Services.Rabbit
{
    public interface IRabbitService
    {
        void runConsumer();
    }
}
