using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    public class AvihangSnackMessage:Entity<long>
    {
        [StringLength(50)]
        [Required]
        public string objectName { get; set; }
        [StringLength(10)]
        [Required]
        public string objectValue { get; set; }
        [StringLength(1)]
        [Required]
        public string type { get; set; }
        [StringLength(200)]
        [Required]
        public string text { get; set; }
    }
}
