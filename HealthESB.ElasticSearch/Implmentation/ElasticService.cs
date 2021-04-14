using HealthESB.ElasticSearch.IContracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthESB.ElasticSearch.Implmentation
{
    
    public class ElasticService: IElasticService
    {
        IConfiguration _configuration;
        private string baseUri = "";
        public ElasticService(IConfiguration configuration)
        {
            this._configuration = configuration;
            baseUri = this._configuration.GetSection("ConnectionStrings").GetSection("ElasticUri").Value;
        }

        public void testElastic()
        {

        }
    }
}
