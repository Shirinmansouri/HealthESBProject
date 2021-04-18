using HealthESB.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{
    public class RedisCacheService : ICacheService
    {
        public void Remove<T>(string cacheKey,T value)
        {
            throw new NotImplementedException();
        }
        public T Set<T>( T value, string cacheKey)
        {
            throw new NotImplementedException();
        }
        public bool TryGet<T,TItem>(string cacheKey, TItem item, out T value)
        {
            throw new NotImplementedException();
        }
    }
}
