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
      
        [DataMember]
        [StringLength(100)]
        public string ActionTitleFr { get; set; }
        [DataMember]
        [StringLength(100)]
        public string ActionTitleEn { get; set; }
        [Required]
        [DataMember]
        [StringLength(100)]
        public string ActionName { get; set; }
        [Required]
        [DataMember]
        [StringLength(100)]
        public int ControllerEntityID { get; set; }
        [Required]
        [DataMember]
        [StringLength(200)]
        public String ControllerName { get; set; }
        [Required]
        [DataMember]
        [StringLength(200)]
        public string ControlleEnTitile { get; set; }
        [Required]
        [DataMember]
        [StringLength(200)]
        public string ControlleFaTitile { get; set; }





    }
}
