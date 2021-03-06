using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class BaseResponse
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }


}
