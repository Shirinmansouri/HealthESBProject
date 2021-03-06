using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class ClaimsResponse : BaseResponse
    {
        public List<ClaimsRow> Claims { get; set; }
        public long LstCount { get; set; }
    }
}
