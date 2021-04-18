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
    public class AvihangSubsciptions : AuditableEntity<int>
    {
        [ForeignKey("AvihangSamadId")]
        public int AvihangSamadId { get; set; }
        public AvihangSamads AvihangSamads { get; set; }
        public int Count { get; set; }
        [StringLength(20)]
        [DataMember]
        [Required]
        public string NationalNumber { get; set; }
        [StringLength(20)]
        [DataMember]
        public string Type { get; set; }
        [StringLength(100)]
        [DataMember]
        public string Description { get; set; }
        [StringLength(50)]
        [DataMember]
        public string Consumption { get; set; }
        [StringLength(50)]
        [DataMember]
        public string ConsumptionInstruction { get; set; }
        public int NumberOfPeriod { get; set; }
        public int BulkId { get; set; }
        [StringLength(500)]
        [DataMember]
        [Required]
        public string CheckCode { get; set; }
        [StringLength(1)]
        public string hasContract { get; set; }
        public int maxCoveredCount { get; set; }
        public bool isAllowed { get; set; }
        public int ResCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string ResMessage { get; set; }
        public DateTime ResDate { get; set; }

    }
}
