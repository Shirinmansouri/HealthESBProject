using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class UserSessionRequest : BaseApiRequest
    {
        public int partnerId { get; set; }
        public int cpartyId { get; set; }
        public UserSessionRequest(int PartnerId, int CpartyId)
        {
            partnerId = PartnerId;
            cpartyId = CpartyId;

        }

    }
}
