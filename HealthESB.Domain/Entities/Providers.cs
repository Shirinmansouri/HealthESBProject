using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{

    public  class Providers:Entity<int>
    {
        [StringLength(200)]
        [DataMember]
        [Required]
        public string Name { get; set; }
        [StringLength(100)]
        [DataMember]
        [Required]
        public string UserName { get; set; }
        [StringLength(100)]
        [DataMember]
        [Required]
        public string Password { get; set; }
        [StringLength(200)]
        [DataMember]
        [Required]
        public string BaseUrl { get; set; }
    }
}
