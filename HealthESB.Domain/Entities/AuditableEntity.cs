using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Entities
{
    [DataContract(IsReference = true)]
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate
        {
            get => DateTime.Now;
            set { }
        }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }
    }
}
