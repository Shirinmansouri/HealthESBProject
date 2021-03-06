using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class ConfirmResponse : BaseResponse
    {
        public int resCode { get; set; } = 0;//کد برگشتی که فقط مقدار 1 بمعنای اوکی میباشد
        public string resMessage { get; set; } = "";//پیام مناسب برگشتی
    }
}
