using HealthESB.Infrastructure.IChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Channel
{
    public  class RestApiAdabtorFactory : IAdaptorFactory
    {
        public T GetWebServiceFactory<T>(string url = "")
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
