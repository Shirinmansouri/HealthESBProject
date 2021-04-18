using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.IChannel
{
    internal interface IAdaptorFactory
    {
        T GetWebServiceFactory<T>(string url = "");
    }
}
