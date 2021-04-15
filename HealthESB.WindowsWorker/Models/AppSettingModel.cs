using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Models
{
    public  class AppSettingModel
    {
        public string InputFolder { get; set; }
        public string RabbitMqUri { get; set; }
        public string ElasticUri { get; set; }
    }
}
