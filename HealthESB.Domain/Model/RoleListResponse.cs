using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
    public class RoleListResponse:BaseResponse
    {
        public List<RoleRow> Roles { get; set; }
        public int LstCount { get; set; }
    }
}
