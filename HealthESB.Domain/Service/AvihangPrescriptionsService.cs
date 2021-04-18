using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Framework.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{
    public class AvihangPrescriptionsService : IAvihangPrescriptionsService
    {
        private IAvihangPrescriptionsRepository _avihangPrescriptionsRepository;
        private ILogService _logService;
        public AvihangPrescriptionsService(IAvihangPrescriptionsRepository avihangPrescriptionsRepository, ILogService logService)
        {
            _avihangPrescriptionsRepository = avihangPrescriptionsRepository;
            _logService = logService;


        }
    }
}
