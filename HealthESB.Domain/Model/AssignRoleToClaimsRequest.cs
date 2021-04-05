using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model
{
  public   class AssignRoleToClaimsRequest
    {
        [Required]
        public string  RoleId { get; set; }
        [Required]
        public List<int> ClaimId { get; set; }
    }
}
