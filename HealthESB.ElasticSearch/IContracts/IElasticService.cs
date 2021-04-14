using System;
using System.Collections.Generic;
using System.Text;

namespace HealthESB.ElasticSearch.IContracts
{
    public interface IElasticService
    {
        System.Threading.Tasks.Task testElasticAsync(string Id);
    }
}
