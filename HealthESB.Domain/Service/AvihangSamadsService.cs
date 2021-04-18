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
    public class AvihangSamadsService : IAvihangSamadsService
    {
        private IAvihangSamadsRepository _organizationsRepository;
        private ILogService _logService;
        public AvihangSamadsService(IAvihangSamadsRepository organizationsRepository, ILogService logService)
        {
            _organizationsRepository = organizationsRepository;
            _logService = logService;


        }
    }
}
