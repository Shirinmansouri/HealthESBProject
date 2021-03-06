using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HealthESB.Domain.Entities
{
    public class PrescriptionBarcode : AuditableEntity<long>
    {
        [ForeignKey("PrescriptionId")]
        public long PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; }
        [Required]
        [DataMember]
        [StringLength(100)]
        public string BarcodeUid { get; set; }
        public decimal  Amount { get; set; }
        [Required]
        [DataMember]
        public string GenericCode { get; set; }
        public int? OutErrorCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string OutErrorMessage { get; set; }
        [ForeignKey("PrescriptionBarcodeStatusId")]
        public int? PrescriptionBarcodeStatusId { get; set; }
        public virtual PrescriptionBarcodeStatus PrescriptionBarcodeStatus { get; set; }
        [StringLength(100)]
        [DataMember]
        public string ReCheckCode { get; set; }

    }


}
