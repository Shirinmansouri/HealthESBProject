using HealthESB.Infrastructure.IChannel;
using HealthESB.Infrastructure.Model.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Infrastructure.Channel
{
    public  class FactoryChannel 
    {
        public   IRestApiChannel GetChannel() => RestApiChannel.GetChannel();
    }
}
