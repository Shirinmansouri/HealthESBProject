using Hangfire;
using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.EF;
using HealthESB.Framework.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Persistance.Repository
{
    public class ProviderApisRepository : GenericRepository<ProviderApis, HealthESBDbContext>,
               IProviderApisRepository
    {
        private readonly static CacheTech cacheTech = CacheTech.Memory;
        private readonly string cacheKey = $"{typeof(ProviderApis)}";
        private readonly Func<CacheTech, ICacheService> _cacheService;
        public ProviderApisRepository(HealthESBDbContext context, Func<CacheTech, ICacheService> cacheService) : base(context)
        {
            _cacheService = cacheService;
        }
        public override async Task<IEnumerable<ProviderApis>> GetAll()
        {
            if (!_cacheService(cacheTech).TryGet(cacheKey,new ProviderApis(), out IReadOnlyList<ProviderApis> cachedList))
            {
                cachedList = await Context.ProviderApis.ToListAsync();
                _cacheService(cacheTech).Set(cachedList, cacheKey);
            }
            return cachedList;
        }
        public override async ValueTask<ProviderApis> GetById(int id)
        {
            if (!_cacheService(cacheTech).TryGet(cacheKey,new ProviderApis(), out IReadOnlyList<ProviderApis> cachedList))
            {
                cachedList = await Context.ProviderApis.ToListAsync();
                _cacheService(cacheTech).Set(cachedList, cacheKey);
            }
            return cachedList.Where(a => a.Id == id).FirstOrDefault();
        }
        public   async Task<IEnumerable<ProviderApis>> GetByProviderId(int ProviderId)
        {
            try
            {
                if (!_cacheService(cacheTech).TryGet(cacheKey,new ProviderApis(), out IReadOnlyList<ProviderApis> cachedList))
                {
                    cachedList = await Context.ProviderApis.ToListAsync();
                    _cacheService(cacheTech).Set(cachedList, cacheKey);
                }
                return cachedList.Where(a => a.ProviderId == ProviderId);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public override async Task Add(ProviderApis entity)
        {
            await Context.Set<ProviderApis>().AddAsync(entity);
            await Context.SaveChangesAsync();
            BackgroundJob.Enqueue(() => RefreshCache(entity));

        }
        public override async Task Update(ProviderApis entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            BackgroundJob.Enqueue(() => RefreshCache(entity));
        }
        public async Task RefreshCache<T>(T value)
        {
            _cacheService(cacheTech).Remove(cacheKey, value);
            var cachedList = await Context.Set<ProviderApis>().ToListAsync();
            _cacheService(cacheTech).Set(cachedList, cacheKey);
        }
    }
}
