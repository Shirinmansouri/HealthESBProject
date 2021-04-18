using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class AvihangSamads : AuditableEntity<int>
    {
        [ForeignKey("AvihangUserSessionId")]
        public int AvihangUserSessionId { get; set; }
        public AvihangUserSessions AvihangUserSessions { get; set; }
        public  AvihangCitizenSessions AvihangCitizenSessions { get; set; }
        [ForeignKey("AvihangCitizenSessionId")]
        public int AvihangCitizenSessionId { get; set; }
        [StringLength(20)]
        [DataMember]
        [Required]
        public string SamadCode { get; set; }
        public int ReceiptPartnerId { get; set; }
        public int ResCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string ResMessage { get; set; }
        public DateTime ResDate { get; set; }
    }
}
