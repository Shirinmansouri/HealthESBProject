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
    public class AvihangUserSessions : AuditableEntity<int>
    {
        public int PartnerId { get; set; }
        public int CpartyId { get; set; }
        [StringLength(20)]
        [DataMember]
        [Required]
        public string SiamID { get; set; }
        [StringLength(20)]
        [DataMember]
        [Required]
        public string MedicalCouncilNumber { get; set; }
        [StringLength(500)]
        [DataMember]
        [Required]
        public string SessionId { get; set; }
        public AvihangUserInfo AvihangUserInfo { get; set; }
        [ForeignKey("AvihangUserInfoId")]
        public int AvihangUserInfoId { get; set; }
        public int ResCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string ResMessage { get; set; }
        public DateTime ResDate { get; set; }
    }
}
