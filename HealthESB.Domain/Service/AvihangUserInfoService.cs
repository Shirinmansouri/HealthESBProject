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
    public class AvihangUserInfoService : IAvihangUserInfoService
    {
        private IAvihangUserInfoRepository _organizationsRepository;
        private ILogService _logService;
        public AvihangUserInfoService(IAvihangUserInfoRepository organizationsRepository, ILogService logService)
        {
            _organizationsRepository = organizationsRepository;
            _logService = logService;


        }
    }
}
