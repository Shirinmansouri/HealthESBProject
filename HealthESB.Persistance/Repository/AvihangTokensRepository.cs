using Hangfire;
using HealthESB.Domain.Entities;
using HealthESB.Domain.IRepository;
using HealthESB.Domain.IService;
using HealthESB.Domain.Model;

using HealthESB.EF;
using HealthESB.EF.DynamicFilter;
using HealthESB.Framework.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthESB.Persistance.Repository
{
    public class AvihangTokensRepository : GenericRepository<AvihangTokens, HealthESBDbContext>,
            IAvihangTokensRepository 
    {
        private readonly static CacheTech cacheTech = CacheTech.Memory;
        private readonly string cacheKey = $"{typeof(AvihangTokens)}";
        private readonly Func<CacheTech, ICacheService> _cacheService;
        public AvihangTokensRepository(HealthESBDbContext context, Func<CacheTech, ICacheService> cacheService) : base(context)
        {
            _cacheService = cacheService;
        }
        public override async Task<IEnumerable<AvihangTokens>> GetAll()
        {
            AvihangTokens avihangTokens = new AvihangTokens();
            if (!_cacheService(cacheTech).TryGet(cacheKey, avihangTokens, out IReadOnlyList<AvihangTokens> cachedList))
            {
                cachedList =await Context.AvihangTokens.Where(a => a.ResCode == 0).ToListAsync();
                _cacheService(cacheTech).Set(cachedList,cacheKey);
            }
            return cachedList;
        }
        public async Task<AvihangTokens> GetByTerminalId(string terminalId)
        {
            try
            {
                AvihangTokens avihangTokens = new AvihangTokens();
                if (!_cacheService(cacheTech).TryGet(cacheKey, avihangTokens, out IReadOnlyList<AvihangTokens> cachedList))
                {
                    cachedList = await Context.AvihangTokens.Where(a => a.ResCode == (int)AvihangResponseCode.Success).ToListAsync();
                    _cacheService(cacheTech).Set(cachedList, cacheKey);
                }
                return cachedList.Where(a => a.TerminalId == terminalId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public override async  Task Add(AvihangTokens entity)
        {
            try
            {
                await Context.Set<AvihangTokens>().AddAsync(entity);
                await RefreshCache(entity);            
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public override async  Task Update(AvihangTokens entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            BackgroundJob.Enqueue(() => RefreshCache(entity));
        }
        public async Task RefreshCache<T>(T value)
        {
            _cacheService(cacheTech).Remove(cacheKey, value);
            var cachedList = await Context.Set<AvihangTokens>().Where(a=>a.ResCode== (int)AvihangResponseCode.Success).ToListAsync();
            _cacheService(cacheTech).Set(cachedList, cacheKey);
        }
    }
}


