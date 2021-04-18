using HealthESB.Domain.IService;
using HealthESB.Domain.Model.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Domain.Service
{

    public class MemoryCacheService : ICacheService
    {
        private readonly IKeyLockerStore _keyLockerStore;
        private readonly IMemoryCache _memoryCache;
        private readonly CacheConfiguration _cacheConfig;
        private MemoryCacheEntryOptions _cacheOptions;
        public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheConfig, IKeyLockerStore keyLockerStore)
        {
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig.Value;
            _keyLockerStore = keyLockerStore;
            if (_cacheConfig != null)
            {
                _cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(_cacheConfig.AbsoluteExpirationInHours),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpirationInMinutes)
                };
            }
        }
        public bool TryGet<T,TItem>(string  key, TItem item, out T value)
        {
            lock (_keyLockerStore.GetLockerFor(key, item))
            {
                _memoryCache.TryGetValue(key, out value);
                if (value == null) return false;
                else return true;
            }
        }
        public T Set<T>(T item, string  key)
        {
            lock (_keyLockerStore.GetLockerFor<T>(key,item))
            {
                return _memoryCache.Set(key, item, _cacheOptions);
            }
        }
        public void Remove<T>(string key,T item)
        {
            lock (_keyLockerStore.GetLockerFor(key,item))
            {
                _memoryCache.Remove(key);
            }
        }
         
    }
}
