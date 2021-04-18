using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class AvihangPrescriptionSubscriptions : Entity<long>
    {
        public AvihangPrescriptions  AvihangPrescriptions { get; set; }
        [ForeignKey("AvihangPrescriptionId")]
        public int AvihangPrescriptionId { get; set; }
        public AvihangSubsciptions AvihangSubsciptions { get; set; }
        [ForeignKey("AvihangSubsciptionId")]
        public int AvihangSubsciptionId { get; set; }
    }
}
