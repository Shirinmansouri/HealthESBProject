using HealthESB.Infrastructure.Model.TTAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model
{
  public  class TTACPrescriptionRequest : BaseRequest
    {

        public string PatientGivenName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientNationalCode { get; set; }
        public string PhysicianGivenName { get; set; }
        public string PhysicianSurname { get; set; }
        public string MedicalCouncilNumber { get; set; }
        public string PharmacyGln { get; set; }
        public int BasicInsurance { get; set; }
        public int ComplementaryInsurance{ get; set; }
    }
 
}
