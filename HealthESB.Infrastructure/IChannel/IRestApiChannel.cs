using HealthESB.Infrastructure.Model.Channel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.IChannel
{
    public  interface IRestApiChannel 
 
    {
        IRestResponse CallWebApi<THeader, TRequest>(THeader header, TRequest Request) where THeader : BaseApiHeader 
            where TRequest : BaseApiRequest ;
 
    }
}
