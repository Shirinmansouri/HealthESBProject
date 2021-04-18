using HealthESB.Domain.IService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{
  public   class KeyLockerStore : IKeyLockerStore
    {
        private readonly ConcurrentDictionary<string, object> _keyLockerMap;
        public KeyLockerStore()
        {
            _keyLockerMap = new ConcurrentDictionary<string, object>();
        }

        public object GetLockerFor<T>(string key,T Value)
        {
            string uniqueKey = $"{ typeof(T)}_{key}";
            return _keyLockerMap.GetOrAdd(uniqueKey, k => new object());
        }

        
    }
}
