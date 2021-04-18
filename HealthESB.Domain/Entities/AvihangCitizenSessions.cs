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
    public class AvihangCitizenSessions:AuditableEntity<int>
    {
        [StringLength(10)]
        [DataMember]
        [Required]
        public string  NationalNumber { get; set; }       
        public Providers providers { get; set; }
        [ForeignKey("ProviderId")]
        public int ProviderId { get; set; }
        [StringLength(500)]
        [DataMember]
        [Required]
        public string CitizenSessionId { get; set; }
        public AvihangUserSessions AvihangUserSessions { get; set; }
        [ForeignKey("AvihangUserSessionId")]
        public int AvihangUserSessionId { get; set; }
        public AvihangPersonInfo AvihangPersonInfo { get; set; }
        [ForeignKey("AvihangPersonInfoId")]
        public long AvihangPersonInfoId { get; set; }
        public int ResCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string ResMessage { get; set; }
        public DateTime ResDate { get; set; }
    }
}
