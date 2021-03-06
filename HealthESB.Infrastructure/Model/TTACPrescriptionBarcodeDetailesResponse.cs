using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model
{
   public class TTACPrescriptionBarcodeDetailesResponse
    {
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public string BatchCode { get; set; }
        public string EnglishName { get; set; }
        public string Expiration { get; set; }
        public string Irc { get; set; }
        public string PersianName { get; set; }
        public int Price { get; set; }
        public string ProductType { get; set; }
        public int TrackingCode { get; set; }
        public bool? UnitConsumed { get; set; }
        public string Manufacturing { get; set; }
        public string GenericCode { get; set; }
        public int ProductTypeId { get; set; }
        public string BarcodeUid { get; set; }
    }
}
