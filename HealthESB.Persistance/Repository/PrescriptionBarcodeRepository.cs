using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.Entities;
using HealthESB.EF;

namespace HealthESB.Persistance.Repository
{
    public class PrescriptionBarcodeRepository : GenericRepository<PrescriptionBarcode, HealthESBDbContext>,
          IPrescriptionBarcodeRepository
    {
        public PrescriptionBarcodeRepository(HealthESBDbContext context) : base(context)
        {
        }
    }
}
