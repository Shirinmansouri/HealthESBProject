using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class AvihangUserInfo : AuditableEntity<int>
    {
        public int internalId { get; set; }
        public int userId { get; set; }
        [StringLength(11)]
        public string cellPhone { get; set; }
        [StringLength(100)]
        public string fullName { get; set; }
        [StringLength(100)]
        public string partnerName { get; set; }
        [StringLength(11)]
        public string partnerNN { get; set; }
        public bool isTwoStep { get; set; }
        [StringLength(1)]
        public string gender { get; set; }
        public int partnerId { get; set; }
        public int cPartyId { get; set; }
    }
}
