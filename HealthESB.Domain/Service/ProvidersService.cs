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
    public class ProvidersService : IProvidersService
    {
        private IProvidersRepository _organizationsRepository;
        private ILogService _logService;
        public ProvidersService(IProvidersRepository organizationsRepository, ILogService logService)
        {
            _organizationsRepository = organizationsRepository;
            _logService = logService;


        }
    }
}
