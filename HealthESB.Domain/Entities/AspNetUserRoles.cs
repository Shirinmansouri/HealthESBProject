using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
   public  class AspNetUserRoles:Entity<string>
    {
        //[ForeignKey("RoleId")]
        public string RoleId { get; set; }
        //public virtual AspNetRoles AspNetRoles { get; set; }
       // [ForeignKey("UserId")]
        public string UserId { get; set; }
        //public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
