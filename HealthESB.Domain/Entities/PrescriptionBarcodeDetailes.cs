using Microsoft.EntityFrameworkCore;
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
   public class PrescriptionBarcodeDetailes:AuditableEntity<long>
    {
        [ForeignKey("PrescriptionBarcodeId")]
        public long PrescriptionBarcodeId { get; set; }
        public virtual PrescriptionBarcode PrescriptionBarcode { get; set; }
        public int Status { get; set; }
        [StringLength(200)]
        [DataMember]
        public string StatusMessage { get; set; }
        [StringLength(10)]
        [DataMember]
        public string BatchCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string EnglishName { get; set; }
        [StringLength(10)]
        [DataMember]
        public string Expiration { get; set; }
        [StringLength(20)]
        [DataMember]
        public string Irc { get; set; }
        [StringLength(100)]
        [DataMember]
        public string PersianName { get; set; }
        public int? Price { get; set; }
        [StringLength(10)]
        [DataMember]
        public string ProductType { get; set; }
        public int? TrackingCode { get; set; }
        public bool? UnitConsumed { get; set; }
        [StringLength(20)]
        [DataMember]
        public string Manufacturing { get; set; }
        [StringLength(10)]
        [DataMember]
        public string GenericCode { get; set; }
        public int? ProductTypeId { get; set; }
        [ForeignKey("PrescriptionBarcodeDetailesTypesId")]
        public int? PrescriptionBarcodeDetailesTypesId { get; set; }
        public virtual PrescriptionBarcodeDetailesType PrescriptionBarcodeDetailesTypes { get; set; }
        [DataMember]
        [StringLength(100)]
        public string BarcodeUid { get; set; }
    }
}
