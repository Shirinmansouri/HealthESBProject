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
    public class AvihangUserSessionsRepository : GenericRepository<AvihangUserSessions, HealthESBDbContext>,
            IAvihangUserSessionsRepository
    {
        private readonly static CacheTech cacheTech = CacheTech.Memory;
        private readonly string cacheKey = $"{typeof(AvihangUserSessions)}";
        private readonly Func<CacheTech, ICacheService> _cacheService;
        public AvihangUserSessionsRepository(HealthESBDbContext context, Func<CacheTech, ICacheService> cacheService) : base(context)
        {
            _cacheService = cacheService;
        }
        public async Task<AvihangUserSessions> GetByPartnerIdAndCpartyId(int PartnerId, int CpartyId)
        {
            try
            {
                AvihangUserSessions avihangTokens = new AvihangUserSessions();
                if (!_cacheService(cacheTech).TryGet(cacheKey, avihangTokens, out IReadOnlyList<AvihangUserSessions> cachedList))
                {
                    cachedList = await Context.AvihangUserSessions.Where(a => a.ResCode == (int)AvihangResponseCode.Success).ToListAsync();
                    _cacheService(cacheTech).Set(cachedList, cacheKey);
                }
                return cachedList.Where(a => a.CpartyId == CpartyId && a.PartnerId == PartnerId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override async Task Add(AvihangUserSessions entity)
        {
            try
            {
                await Context.Set<AvihangUserSessions>().AddAsync(entity);
                await RefreshCache(entity);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public override async Task Update(AvihangUserSessions entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            await RefreshCache(entity);
        }
        public async Task RefreshCache<T>(T value)
        {
            _cacheService(cacheTech).Remove(cacheKey, value);
            var cachedList = await Context.Set<AvihangUserSessions>().Where(a => a.ResCode == (int)AvihangResponseCode.Success).ToListAsync();
            _cacheService(cacheTech).Set(cachedList, cacheKey);
        }

    }
}


