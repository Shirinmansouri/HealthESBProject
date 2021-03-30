using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
  public   class AssignRoleToClaimsRequest
    {
        public string RoleId { get; set; }
        public int ClaimId { get; set; }
    }
}
