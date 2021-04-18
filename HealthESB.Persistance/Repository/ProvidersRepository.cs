using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthESB.Domain.IRepository;
using HealthESB.EF;
using HealthESB.Domain.Entities;
using HealthESB.Domain.IService;
using HealthESB.Framework.Utility;
using Microsoft.EntityFrameworkCore;
using Hangfire;

namespace HealthESB.Persistance.Repository
{
    public class ProvidersRepository : GenericRepository<Providers, HealthESBDbContext>,
              IProvidersRepository
    {
        private readonly static CacheTech cacheTech = CacheTech.Memory;
        private readonly string cacheKey = $"{typeof(Providers)}";
        private readonly Func<CacheTech, ICacheService> _cacheService;
        public ProvidersRepository(HealthESBDbContext context, Func<CacheTech, ICacheService> cacheService) : base(context)
        {
            _cacheService = cacheService;
        }
        public override async Task<IEnumerable<Providers>> GetAll()
        {
            if (!_cacheService(cacheTech).TryGet(cacheKey,new Providers(), out IReadOnlyList<Providers> cachedList))
            {
                cachedList = await  Context.Providers.ToListAsync();
                _cacheService(cacheTech).Set(cachedList, cacheKey);
            }
            return cachedList;
        }
        public override async ValueTask<Providers> GetById(int id)
        {
            try
            {
            if (!_cacheService(cacheTech).TryGet(cacheKey,new Providers(), out IReadOnlyList<Providers> cachedList))
            {
                cachedList = await Context.Providers.ToListAsync();
                _cacheService(cacheTech).Set(cachedList, cacheKey);
            }
            return cachedList.Where(a => a.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public override async Task Add(Providers entity)
        {
            await Context.Set<Providers>().AddAsync(entity);
            await Context.SaveChangesAsync();
            BackgroundJob.Enqueue(() => RefreshCache(entity));

        }
        public override async Task Update(Providers entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            BackgroundJob.Enqueue(() => RefreshCache(entity));
        }
        public async Task RefreshCache<T>(T value)
        {
            _cacheService(cacheTech).Remove(cacheKey,value);
            var cachedList = await Context.Set<Providers>().ToListAsync();
            _cacheService(cacheTech).Set(cachedList, cacheKey);
        }
    }
}
