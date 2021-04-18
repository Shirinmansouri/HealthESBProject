using HealthESB.Infrastructure.Model.TTAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model
{
   public class TTACReactiveRequest:BaseRequest
    {
        public string BarcodeUid { get; set; }
        public decimal Amount { get; set; }
        public int TrackingCode { get; set; }
        public string PharmacyGln { get; set; }
        public int Count { get; set; } = 0;//تعداد دارو



    }

  
}

 
