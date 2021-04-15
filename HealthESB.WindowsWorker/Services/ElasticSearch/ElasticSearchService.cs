using HealthESB.WindowsWorker.Models;
using HealthESB.WindowsWorker.Models.ElasticModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Services.ElasticSearch
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly AppSettingModel _settings;
        public ElasticSearchService(IOptions<AppSettingModel> settings)
        {
            _settings = settings.Value;
        }
        public ElasticResponeModel RegisterApi(ElasticRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
