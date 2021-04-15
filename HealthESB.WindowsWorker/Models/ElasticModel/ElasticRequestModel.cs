using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Models.ElasticModel
{
    public class ElasticRequestModel
    {
        public string id { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public string apiname { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string requestObj { get; set; }
        public string responseObj { get; set; }

    }
}
