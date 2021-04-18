using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class Info
    {
        public string sessionId { get; set; }
        public bool isCovered { get; set; }
        public int age { get; set; }
        public string birthDate { get; set; }
        public int countOfFamilyMember { get; set; }
        public string gender { get; set; }
        public string geoInfo { get; set; }
        public string lastName { get; set; }
        public string lifeStatus { get; set; }
        public string name { get; set; }
        public string memberImage { get; set; }
        public bool productName { get; set; }
        public string registrationStatus { get; set; }
        public string relationType { get; set; }
        public string responsibleFullName { get; set; }
        public string responsibleNN { get; set; }
        public string zipCode { get; set; }
        public string nationalNumber { get; set; }
        public string accountValidto { get; set; }
        public string issuerType { get; set; }
        public int productId { get; set; }
        public bool isReferenceable { get; set; }
        public string cellPhoneNumber { get; set; }
        public string maritalStatus { get; set; }
        public int coverageReferenceInter { get; set; }
        public string specialAccount { get; set; }
        public Message message { get; set; }
        public string samadCode { get; set; }
        public string citizenSessionId { get; set; }
        //public List<AdditionalProperties> additionalProperties;
        //public List<AccessNodes> accessNodes;
        //public List<ContractingPartyLicense> contractingPartyLicenses;
        public int internalId { get; set; }
        public int userId { get; set; }
        public string cellPhone { get; set; }
        public string fullName { get; set; }
        public string partnerName { get; set; }
        public string partnerNN { get; set; }
        public int partnerId { get; set; }
        public int cPartyId { get; set; }
        public bool isTwoStep { get; set; }
        public string token { get; set; }
        public int ttl { get; set; }
        public string dto { get; set; }
        public string checkCode { get; set; }
        public string hasContract { get; set; }
        public string maxCoveredCount { get; set; }
        public bool isAllowed { get; set; }
        public string trackingCode { get; set; }
        public int sequenceNumber { get; set; }
        public string HID { get; set; }
        public int terminalId { get; set; }


    }
}
