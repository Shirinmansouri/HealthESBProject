using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class PrescriptionBarcodeResponse :BaseResponse
    {

        public List<PrescriptionBarcodeDetailesResponse> ItemsInfo { get; set; }
        public long PrescriptionId { get; set; }


    }
}
