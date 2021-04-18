using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class SamadRequest :BaseApiRequest
    {
        public string cpartySessionId { get; set; }
        public string receiptPartnerId { get; set; }
        public string citizenSessionId { get; set; }

    }
}
