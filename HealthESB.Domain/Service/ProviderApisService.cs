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
    public class ProviderApisService : IProviderApisService
    {
        private IProviderApisRepository _organizationApisRepository;
        private ILogService _logService;
        public ProviderApisService(IProviderApisRepository organizationApisRepository, ILogService logService)
        {
            _organizationApisRepository = organizationApisRepository;
            _logService = logService;


        }
    }
}
