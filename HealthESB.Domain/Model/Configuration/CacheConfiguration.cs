using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthESB.Domain.Model.Configuration
{
    public class CacheConfiguration
    {
        public int AbsoluteExpirationInHours { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
    }
}
