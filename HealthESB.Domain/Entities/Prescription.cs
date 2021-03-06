using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace HealthESB.Domain.Entities
{


    public class Prescription : AuditableEntity<long>
    {
        [StringLength(100)]
        [DataMember]
        [Required]
        public string PatientGivenName { get; set; }
        [Required]
        [StringLength(100)]
        [DataMember]
        public string PatientSurname { get; set; }
        [Required]
        [StringLength(100)]
        [DataMember]
        public string PatientNationalCode { get; set; }
        [Required]
        [StringLength(100)]
        [DataMember]
        public string PhysicianGivenName { get; set; }
        [Required]
        [StringLength(100)]
        [DataMember]
        public string PhysicianSurName { get; set; }
        [Required]
        [StringLength(100)]
        [DataMember]
        public string MedicalCouncilNumber { get; set; }
        [Required]
        [StringLength(100)]
        [DataMember]
        public string PharmacyGln { get; set; }
        public int BasicInsurance { get; set; }
        public int ComplementaryInsurance { get; set; }
        public long? OutPrescriptionId { get; set; }
        public int? OutErrorCode { get; set; }
        [StringLength(100)]
        [DataMember]
        public string OutErrorMessage { get; set; }

        
    }
}
