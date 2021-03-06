using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class PrescriptionBarcodeRequest : BaseRequest
    {
       
        public long   PrescriptionId { get; set; }
        public string BarcodeUid { get; set; }
        public decimal Amount { get; set; }
        public string GenericCode { get; set; }
        public string ReCheckCode { get; set; }
    }
}
