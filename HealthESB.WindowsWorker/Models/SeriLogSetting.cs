using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Models
{
    public class SeriLogSetting
    {
        public string loggerTemplate { get; set; }
        public string baseDir { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        public string App_Data { get; set; }
        public string logs { get; set; }
        public string log { get; set; }
    }
}
