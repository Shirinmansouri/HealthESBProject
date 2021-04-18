using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class Header : BaseApiHeader
    {      
        public Header(string apiUrl, int apiMethodType = 1, string contentType = "application/json") :base(apiUrl,apiMethodType,contentType)
        {

        }
        public string token { get; set; }
        public int terminalId { get; set; }
        public string clientIPAddress { get; set; }
        public string clientAgentInfo { get; set; }

    }
}
