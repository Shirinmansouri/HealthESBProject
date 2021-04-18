using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.Model;
using HealthESB.EF;
using HealthESB.EF.DynamicFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Persistance.Repository
{
    public class AvihangSamadsRepository : GenericRepository<AvihangSamads, HealthESBDbContext>,
            IAvihangSamadsRepository
    {
        public AvihangSamadsRepository(HealthESBDbContext context) : base(context)
        {
        }

       
    }
}


