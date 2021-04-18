using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class SubscriptionRequest :BaseApiRequest
    {
        public string cpartySessionId { get; set; }
        public string citizenSessionId { get; set; }
        public string samadCode { get; set; }
        public Service service { get; set; }
        public OtherServices otherServices { get; set; }
    }
}
