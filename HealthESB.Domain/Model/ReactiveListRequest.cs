using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class LongListRequest
    {
        public List<long?> LstPrescriptionBarcodeDetailesId { get; set; }
    }
}
