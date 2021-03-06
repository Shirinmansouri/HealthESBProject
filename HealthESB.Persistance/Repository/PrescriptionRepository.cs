using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Persistance.Repository
{
    public class PrescriptionRepository : GenericRepository<Prescription, HealthESBDbContext>,
         IPrescriptionRepository
    {
        public PrescriptionRepository(HealthESBDbContext context) : base(context)
        {
        }
    }
}
