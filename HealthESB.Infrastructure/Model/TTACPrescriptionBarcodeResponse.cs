﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Model
{
    public class TTACPrescriptionBarcodeResponse
    {
        public long PrescriptionId { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<TTACPrescriptionBarcodeDetailesResponse> ItemsInfo { get; set; }
    }
}
