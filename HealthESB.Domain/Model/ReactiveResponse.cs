using HealthESB.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class ReactiveResponse:BaseResponse
    {
        public long PrescriptionId { get; set; }
        public List<PrescriptionBarcodeDetailesResponse> ItemsInfo { get; set; }
    }
}
