using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class AvihangPrescriptions : AuditableEntity<long>
    {
        [StringLength(20)]
        public string ReferenceFeedback { get; set; }
        public AvihangSamads AvihangSamads { get; set; }
        [ForeignKey("AvihangSamadId")]
        public int AvihangSamadId { get; set; }
        [StringLength(10)]
        public string TrackingCode { get; set; }
        public int SequenceNumber { get; set; }
        [StringLength(10)]
        public string HID { get; set; }
        public int ResCode { get; set; }
        [StringLength(100)]
        public string ResMessage { get; set; }
        public DateTime ResDate { get; set; }
    }
}
