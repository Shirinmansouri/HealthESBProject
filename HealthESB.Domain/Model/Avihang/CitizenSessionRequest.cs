using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class CitizenSessionRequest:BaseRequest
    {

        public string cpartySessionId { get; set; }
        public string nationalNumber { get; set; }
        public string citizenSessionId { get; set; }
    }
}
