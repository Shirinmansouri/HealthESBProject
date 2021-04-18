using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class AvihangPersonInfo:AuditableEntity<long>
    {

        public bool isCovered { get; set; }
        public int age { get; set; }
        [StringLength(10)]
        public string birthDate { get; set; }
        public int countOfFamilyMember { get; set; }
        [StringLength(1)]
        public string gender { get; set; }
        [StringLength(50)]
        public string geoInfo { get; set; }
        [StringLength(100)]
        public string lastName { get; set; }
        [StringLength(10)]
        public string lifeStatus { get; set; }
        [StringLength(100)]
        public string name { get; set; }
        [StringLength(1000)]
        public string memberImage { get; set; }
        public bool productName { get; set; }
        [StringLength(10)]
        public string registrationStatus { get; set; }
        [StringLength(1)]
        public string relationType { get; set; }
        [StringLength(100)]
        public string responsibleFullName { get; set; }
        [StringLength(11)]
        public string responsibleNN { get; set; }
        [StringLength(10)]
        public string zipCode { get; set; }
        [StringLength(11)]
        public string nationalNumber { get; set; }
        [StringLength(10)]
        public string accountValidto { get; set; }
        [StringLength(1)]
        public string issuerType { get; set; }
        public int productId { get; set; }
        public bool isReferenceable { get; set; }
        [StringLength(11)]
        public string cellPhoneNumber { get; set; }
        [StringLength(1)]
        public string maritalStatus { get; set; }
        public int coverageReferenceInter { get; set; }
        [StringLength(50)]
        public string specialAccount { get; set; }

    }
}
