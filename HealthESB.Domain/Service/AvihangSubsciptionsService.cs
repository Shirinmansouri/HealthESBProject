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
    public class AvihangSubsciptionsService : IAvihangSubsciptionsService
    {
        private IAvihangSubsciptionsRepository _organizationsRepository;
        private ILogService _logService;
        public AvihangSubsciptionsService(IAvihangSubsciptionsRepository organizationsRepository, ILogService logService)
        {
            _organizationsRepository = organizationsRepository;
            _logService = logService;


        }
    }
}
