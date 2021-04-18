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
    public class AvihangTokens:AuditableEntity<int>
    {
 
        [StringLength(10)]
        [DataMember]
        [Required]
        public string TerminalId { get; set; }
        [StringLength(500)]
        [DataMember]
        public string Token { get; set; }
        public int ttl { get; set; }
        public string  dto { get; set; }
        public int ResCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string ResMessage { get; set; }
        public DateTime ResDate { get; set; }
    }
}
