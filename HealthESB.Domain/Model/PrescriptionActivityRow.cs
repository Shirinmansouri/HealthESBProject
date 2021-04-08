using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class PrescriptionActivityRow
    {
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public string BatchCode { get; set; }
        public string EnglishName { get; set; }
        public string Expiration { get; set; }
        public string Irc { get; set; }
        public string PersianName { get; set; }
        public int? Price { get; set; }
        public string ProductType { get; set; }
        public int? TrackingCode { get; set; }
        public bool? UnitConsumed { get; set; }
        public string Manufacturing { get; set; }
        public string GenericCode { get; set; }
        public int? ProductTypeId { get; set; }
        public string BarcodeUid { get; set; }
        public long PrescriptionBarcodeId { get; set; }
        public long PrescriptionId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? PrescriptionBarcodeDetailesTypesId { get; set; }
        public int? PrescriptionBarcodeStatusId { get; set; }
        public string PrescriptionBarcodeDetailesTypesName { get; set; }
        public string PrescriptionBarcodeStatusName { get; set; }
        public string PatientGivenName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientNationalCode { get; set; }
        public string PhysicianGivenName { get; set; }
        public string PhysicianSurname { get; set; }
        public string MedicalCouncilNumber { get; set; }
        public string PharmacyGln { get; set; }
        public int BasicInsurance { get; set; }
        public int ComplementaryInsurance { get; set; }
        public string Uid { get; set; }
        public decimal? Amount { get; set; }
        public long? OutPrescriptionId { get; set; }

    }
}
