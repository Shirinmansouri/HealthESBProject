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
    public class AvihangSnackMessageService : IAvihangSnackMessageService
    {
        private IAvihangSnackMessageRepository _avihangSnackMessageRepository;
        private ILogService _logService;
        public AvihangSnackMessageService(IAvihangSnackMessageRepository avihangSnackMessageRepository, ILogService logService)
        {
            _avihangSnackMessageRepository = avihangSnackMessageRepository;
            _logService = logService;


        }
    }
}
