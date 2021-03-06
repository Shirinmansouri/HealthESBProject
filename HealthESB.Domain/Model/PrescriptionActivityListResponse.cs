using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public  class PrescriptionActivityListResponse:BaseResponse
    {
        public List<PrescriptionActivityRow> LstPrescriptionActivityRow { get; set; }
        public long LstCount { get; set; }
    }
}
