using HealthESB.WindowsWorker.Models.ElasticModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.WindowsWorker.Services.ElasticSearch
{
    public interface IElasticSearchService
    {
        ElasticResponeModel RegisterApi(ElasticRequestModel request);
    }
}
