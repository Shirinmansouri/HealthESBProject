using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class ProviderApis : Entity<int>
    {
        [StringLength(200)]
        [DataMember]
        [Required]
        public string Url { get; set; }
        public Providers Providers { get; set; }
        [ForeignKey("ProviderId")]
        public int ProviderId { get; set; }
        [StringLength(200)]
        [DataMember]
        [Required]
        public string Key { get; set; }
        [StringLength(200)]
        [DataMember]
        [Required]
        public string Name { get; set; }
    }
}
