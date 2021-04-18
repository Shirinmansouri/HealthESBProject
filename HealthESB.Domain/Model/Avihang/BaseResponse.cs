using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Avihang
{
    public class BaseResponse
    {
        public int resCode { get; set; }
        public string resMessage { get; set; }
        public Info info { get; set; }


    }
}
