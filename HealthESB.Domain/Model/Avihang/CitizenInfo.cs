using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class CitizenInfo
    {
        public bool isCovered { get; set; }
        public int age { get; set; }
        public string birthDate { get; set; }
        public int countOfFamilyMember { get; set; }
        public string gender { get; set; }
        public string geoInfo { get; set; }
        public string lastName { get; set; }
        public string lifeStatus { get; set; }
        public string name { get; set; }
        public byte[] memberImage { get; set; }
        public bool productName { get; set; }
        public string  registrationStatus { get; set; }
        public string relationType { get; set; }
        public string  responsibleFullName { get; set; }
        public string responsibleNN { get; set; }
        public string zipCode { get; set; }
        public string nationalNumber { get; set; }
        public string accountValidto { get; set; }
        public string issuerType { get; set; }
        public string productId { get; set; }
        public bool isReferenceable { get; set; }
        public string cellPhoneNumber { get; set; }
        public string maritalStatus { get; set; }
        public Message message { get; set; }
        public string samadCode { get; set; }
        public string citizenSessionId { get; set; }


    }
}
