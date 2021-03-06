using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class Claims : Entity<int>
    {
        [Required]
        [DataMember]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [DataMember]
        [StringLength(100)]
        public string Value { get; set; }
    }
}
